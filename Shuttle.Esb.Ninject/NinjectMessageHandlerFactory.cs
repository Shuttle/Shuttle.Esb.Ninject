using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Ninject
{
    public class NinjectMessageHandlerFactory : MessageHandlerFactory, IRequireInitialization
    {
        private readonly Dictionary<Type, Type> _messageHandlerTypes = new Dictionary<Type, Type>();
        private static readonly Type MessageHandlerType = typeof(IMessageHandler<>);
        private readonly StandardKernel _kernel;
        private readonly ILog _log;
        private readonly ReflectionService _reflectionService = new ReflectionService();

        public NinjectMessageHandlerFactory(StandardKernel kernel)
        {
            Guard.AgainstNull(kernel, "kernel");

            _kernel = kernel;

            _log = Log.For(this);
        }

        public override object CreateHandler(object message)
        {
            var all = _kernel.GetAll(MessageHandlerType.MakeGenericType(message.GetType())).ToList();

            return all.Count != 0 ? all[0] : null;
        }

        public override IMessageHandlerFactory RegisterHandlers(Assembly assembly)
        {
            try
            {
                foreach (var type in _reflectionService.GetTypes(MessageHandlerType, assembly))
                {
                    RegisterHandler(type);
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(string.Format(EsbResources.RegisterHandlersException, assembly.FullName,
                    ex.AllMessages()));

                throw;
            }

            return this;
        }

        public override IMessageHandlerFactory RegisterHandler(Type type)
        {
            Guard.AgainstNull(type,"type");
            
            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsAssignableTo(MessageHandlerType))
                {
                    continue;
                }

                var messageType = @interface.GetGenericArguments()[0];

                if (!_messageHandlerTypes.ContainsKey(messageType))
                {
                    _messageHandlerTypes.Add(messageType, type);
                }
                else
                {
                    throw new InvalidOperationException(string.Format(NinjectResources.DuplicateMessageHandlerIgnored, _messageHandlerTypes[messageType].FullName, messageType.FullName, type.FullName));
                }

                _kernel.Bind(MessageHandlerType.MakeGenericType(messageType)).To(type).InTransientScope();
            }

            return this;
        }

        public override IEnumerable<Type> MessageTypesHandled
        {
            get { return _messageHandlerTypes.Keys; }
        }

        public void Initialize(IServiceBus bus)
        {
            Guard.AgainstNull(bus, "bus");

            if (!_kernel.GetBindings(typeof(IServiceBus)).Any())
            {
                _kernel.Bind<IServiceBus>().ToConstant(bus);
            }
        }

        public override void ReleaseHandler(object handler)
        {
            base.ReleaseHandler(handler);

            _kernel.Release(handler);
        }
    }
}

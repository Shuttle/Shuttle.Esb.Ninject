using System;

namespace Shuttle.Esb.Ninject.Tests
{
    public class SimpleMessageHandler : IMessageHandler<SimpleCommand>
    {
        public void ProcessMessage(IHandlerContext<SimpleCommand> context)
        {
            Console.WriteLine("[simple command] : name = '{0}'", context.Message.Name);
        }

        public bool IsReusable {
            get { return true; }
        }
    }
}
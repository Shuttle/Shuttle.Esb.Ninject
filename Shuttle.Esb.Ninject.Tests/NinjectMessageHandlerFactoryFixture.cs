using System;
using System.Linq;
using Ninject;
using NUnit.Framework;
using Shuttle.Esb.Ninject.Tests.Duplicate;

namespace Shuttle.Esb.Ninject.Tests
{
    [TestFixture]
    public class NinjectMessageHandlerFactoryFixture
    {
        [Test]
        public void Should_be_able_to_find_message_handlers()
        {
            var kernel = new StandardKernel();

            var factory = new NinjectMessageHandlerFactory(kernel);

            factory.RegisterHandlers(GetType().Assembly);

            Assert.IsTrue(factory.MessageTypesHandled.Contains(typeof (SimpleCommand)));
            Assert.IsTrue(factory.MessageTypesHandled.Contains(typeof (SimpleEvent)));
            Assert.IsNotNull(factory.CreateHandler(new SimpleCommand()));
            Assert.IsNotNull(factory.CreateHandler(new SimpleEvent()));
        }


        [Test]
        public void Should_fail_when_attempting_to_register_duplicate_handlers()
        {
            var kernel = new StandardKernel();

            var factory = new NinjectMessageHandlerFactory(kernel);

            Assert.Throws<InvalidOperationException>(() => factory.RegisterHandlers(typeof(DuplicateCommand).Assembly));
        }
    }
}
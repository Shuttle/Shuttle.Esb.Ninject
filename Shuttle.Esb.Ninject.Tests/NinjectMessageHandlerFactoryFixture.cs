using System.Linq;
using Ninject;
using NUnit.Framework;

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
    }
}
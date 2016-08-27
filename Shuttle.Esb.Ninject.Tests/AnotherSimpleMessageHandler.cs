namespace Shuttle.Esb.Ninject.Tests
{
    public class AnotherSimpleMessageHandler : 
        IMessageHandler<SimpleCommand>,
        IMessageHandler<SimpleEvent>
    {
        public void ProcessMessage(IHandlerContext<SimpleCommand> context)
        {
        }

        public void ProcessMessage(IHandlerContext<SimpleEvent> context)
        {
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
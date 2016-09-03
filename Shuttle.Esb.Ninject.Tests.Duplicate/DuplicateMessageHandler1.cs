namespace Shuttle.Esb.Ninject.Tests.Duplicate
{
    public class DuplicateMessageHandler1 : IMessageHandler<DuplicateCommand>
    {
        public void ProcessMessage(IHandlerContext<DuplicateCommand> context)
        {
        }
    }
}
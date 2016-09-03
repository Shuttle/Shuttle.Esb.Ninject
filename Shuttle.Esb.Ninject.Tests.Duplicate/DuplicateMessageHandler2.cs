namespace Shuttle.Esb.Ninject.Tests.Duplicate
{
    public class DuplicateMessageHandler2 : IMessageHandler<DuplicateCommand>
    {
        public void ProcessMessage(IHandlerContext<DuplicateCommand> context)
        {
        }
    }
}
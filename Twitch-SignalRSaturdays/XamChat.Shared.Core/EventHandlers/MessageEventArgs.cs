namespace XamChat.Core.EventHandlers
{
    public class MessageEventArgs : IMessageEventArgs
    {
        public MessageEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}

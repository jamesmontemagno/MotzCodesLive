namespace XamChat.Core.EventHandlers
{
    public class MessageEventArgs
    {
        public MessageEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}

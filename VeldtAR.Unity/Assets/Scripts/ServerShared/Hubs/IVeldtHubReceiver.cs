using ServerShared.MessagePackObjects;


namespace ServerShared.Hubs
{
    /// <summary>
    /// Server -> Client API
    /// </summary>
    public interface IVeldtHubReceiver
    {
        void OnJoin();

        void OnLeave();

//        void OnSendMessage(MessageResponse message);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

using MagicOnion;
using ServerShared.MessagePackObjects;


namespace ServerShared.Hubs
{
    /// <summary>
    /// Client -> Server API (Streaming)
    /// </summary>
    public interface IVeldtHub : IStreamingHub<IVeldtHub, IVeldtHubReceiver>
    {
        Task JoinAsync(JoinRequest request);

        Task LeaveAsync();

//        Task SendMessageAsync(string message);

        Task GenerateException(string message);

        // It is not called because it is a method as a sample of arguments.
        Task SampleMethod(List<int> sampleList, Dictionary<int, string> sampleDictionary);
    }
}

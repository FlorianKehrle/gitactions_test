using MagicOnion;
using MessagePack;

namespace ServerShared.Services
{
    /// <summary>
    /// Client -> Server API
    /// </summary>
    public interface IVeldtService : IService<IVeldtService>
    {
        UnaryResult<Nil> GenerateException(string message);
        UnaryResult<Nil> SendReportAsync(string message);
    }
}

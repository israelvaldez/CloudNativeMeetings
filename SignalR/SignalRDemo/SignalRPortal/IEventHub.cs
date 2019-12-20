using System.Threading.Tasks;

namespace SignalRPortal
{
    public interface IEventHub
    {
        Task SendNoticeEventToClient(string message);
    }
}

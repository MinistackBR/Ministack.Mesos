using Ministack.Mesos.Abstractions.Data;
using System.Threading.Tasks;

namespace Ministack.Mesos.Abstractions.Handlers
{
    public interface INotificationHandler<TNotification> where TNotification : INotification
    {
        Task HandleAsync(TNotification notification);
    }
}

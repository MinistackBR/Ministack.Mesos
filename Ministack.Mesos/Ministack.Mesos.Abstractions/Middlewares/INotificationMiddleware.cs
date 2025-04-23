using Ministack.Mesos.Abstractions.Data;
using System;
using System.Threading.Tasks;

namespace Ministack.Mesos.Abstractions.Middlewares
{
    public interface INotificationMiddleware<TNotification> where TNotification : INotification
    {
        Task ProcessAsync(TNotification notification, Func<TNotification, Task> next);
    }
}

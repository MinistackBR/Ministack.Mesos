using Ministack.Mesos.Abstractions.Data;
using Ministack.Mesos.Abstractions.Handlers;
using Ministack.Mesos.Abstractions.Middlewares;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ministack.Mesos.Pipelining
{
    internal class NotificationPipeline<TNotification> where TNotification : INotification
    {
        private readonly List<INotificationMiddleware<TNotification>> middlewares = new List<INotificationMiddleware<TNotification>>();
        private readonly List<INotificationHandler<TNotification>> handlers = new List<INotificationHandler<TNotification>>();

        internal NotificationPipeline<TNotification> AddMiddleware(INotificationMiddleware<TNotification> middleware)
        {
            middlewares.Add(middleware);
            return this;
        }

        internal NotificationPipeline<TNotification> SetHandler(INotificationHandler<TNotification> handler)
        {
            handlers.Add(handler);
            return this;
        }

        internal async Task ExecuteAsync(TNotification notification)
        {
            Func<TNotification, Task> next = async (req) =>
            {
                foreach (var handler in handlers)
                {
                    await handler.HandleAsync(req);
                }
            };

            foreach (var middleware in middlewares)
            {
                next = (req) => middleware.ProcessAsync(req, next);
            }

            await next(notification);
        }
    }
}

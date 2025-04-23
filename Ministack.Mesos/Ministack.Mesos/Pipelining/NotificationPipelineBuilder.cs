using Ministack.Mesos.Abstractions.Data;
using Ministack.Mesos.Abstractions.Handlers;
using Ministack.Mesos.Abstractions.Middlewares;

namespace Ministack.Mesos.Pipelining
{
    public class NotificationPipelineBuilder<TNotification> where TNotification : INotification
    {
        private readonly NotificationPipeline<TNotification> _pipeline;

        internal NotificationPipelineBuilder()
        {
            _pipeline = new NotificationPipeline<TNotification>();
        }

        public NotificationPipelineBuilder<TNotification> Use(INotificationMiddleware<TNotification> middleware)
        {
            _pipeline.AddMiddleware(middleware);
            return this;
        }

        public NotificationPipelineBuilder<TNotification> HandleWith(INotificationHandler<TNotification> handler)
        {
            _pipeline.SetHandler(handler);
            return this;
        }

        internal NotificationPipeline<TNotification> Build()
            => _pipeline;
    }
}

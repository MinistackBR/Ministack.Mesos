using Ministack.Mesos.Abstractions;
using Ministack.Mesos.Abstractions.Data;
using Ministack.Mesos.Pipelining;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ministack.Mesos
{
    public class Mediator : IMediator
    {
        private Dictionary<string, object> PipelinePerRequestType { get; } = new Dictionary<string, object>();
        private  Dictionary<string, object> PipelinePerNotificationType { get; } = new Dictionary<string, object>();

        #region Registration
        internal void Register<TRequest, TResponse>(RequestPipeline<TRequest, TResponse> pipeline) where TRequest : IRequest<TResponse> where TResponse : class
        {
            var key = typeof(TRequest).FullName + typeof(TResponse).FullName;
            PipelinePerRequestType[key] = pipeline;
        }

        internal void Register<TNotification>(NotificationPipeline<TNotification> pipeline) where TNotification : INotification
        {
            var key = typeof(TNotification).FullName;
            PipelinePerNotificationType[key] = pipeline;
        }
        #endregion

        public async Task NotifyAsync<TNotification>(TNotification notification) where TNotification : INotification
        {
            var key = typeof(TNotification).FullName;

            if (!PipelinePerNotificationType.TryGetValue(key, out var pipelineObj))
            {
                return;
            }

            if (!(pipelineObj is NotificationPipeline<TNotification> pipeline))
            {
                return;
            }

            await pipeline.ExecuteAsync(notification);
        }  

        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse> where TResponse : class
        {
            var key = typeof(TRequest).FullName + typeof(TResponse).FullName;

            if (!PipelinePerRequestType.TryGetValue(key, out var pipelineObj))
            {
                throw new KeyNotFoundException($"No pipeline found for request type {key}.");
            }

            if (!(pipelineObj is RequestPipeline<TRequest, TResponse> pipeline))
            {
                throw new InvalidCastException($"Pipeline for request type {key} is not of the expected type.");
            }

            return pipeline.ExecuteAsync(request);
        }
    }
}

using Ministack.Mesos.Abstractions;
using Ministack.Mesos.Abstractions.Data;
using Ministack.Mesos.Pipelining;
using System;

namespace Ministack.Mesos
{
    public class MediatorConfigurator
    {
        private readonly Mediator _mediator;

        public MediatorConfigurator()
        {
            _mediator = new Mediator();
        }

        public MediatorConfigurator HasRequest<TRequest, TResponse>(Action<RequestPipelineBuilder<TRequest, TResponse>> configuration = null) where TRequest : IRequest<TResponse> where TResponse : class
        {
            var builder = new RequestPipelineBuilder<TRequest, TResponse>();

            configuration?.Invoke(builder);
            var pipeline = builder.Build();
            _mediator.Register(pipeline);

            return this;
        }

        public MediatorConfigurator HasNotification<TNotification>(Action<NotificationPipelineBuilder<TNotification>> configuration = null) where TNotification : INotification
        {
            var builder = new NotificationPipelineBuilder<TNotification>();

            configuration?.Invoke(builder);
            var pipeline = builder.Build();
            _mediator.Register(pipeline);

            return this;
        }
    }
}

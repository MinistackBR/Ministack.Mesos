using Ministack.Mesos.Abstractions.Data;
using Ministack.Mesos.Abstractions.Handlers;
using Ministack.Mesos.Abstractions.Middlewares;

namespace Ministack.Mesos.Pipelining
{
    public class RequestPipelineBuilder<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : class
    {
        private readonly RequestPipeline<TRequest, TResponse> _pipeline;

        internal RequestPipelineBuilder()
        {
            _pipeline = new RequestPipeline<TRequest, TResponse>();
        }

        public RequestPipelineBuilder<TRequest, TResponse> Use(IRequestMiddleware<TRequest, TResponse> middleware)
        {
            _pipeline.AddMiddleware(middleware);
            return this;
        }

        public void HandleWith(IRequestHandler<TRequest, TResponse> handler)
        {
            _pipeline.SetHandler(handler);
        }

        internal RequestPipeline<TRequest, TResponse> Build() 
            => _pipeline;
    }
}

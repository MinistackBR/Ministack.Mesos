using Ministack.Mesos.Abstractions.Data;
using Ministack.Mesos.Abstractions.Handlers;
using Ministack.Mesos.Abstractions.Middlewares;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ministack.Mesos.Pipelining
{
    internal class RequestPipeline<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : class
    {
        private readonly List<IRequestMiddleware<TRequest, TResponse>> middlewares = new List<IRequestMiddleware<TRequest, TResponse>>();
        private IRequestHandler<TRequest, TResponse> handler;

        internal RequestPipeline<TRequest, TResponse> AddMiddleware(IRequestMiddleware<TRequest, TResponse> middleware)
        {
            middlewares.Add(middleware);
            return this;
        }

        internal RequestPipeline<TRequest, TResponse> SetHandler(IRequestHandler<TRequest, TResponse> handler)
        {
            this.handler = handler;
            return this;
        }

        internal async Task<TResponse> ExecuteAsync(TRequest request)
        {
            if (handler == null)
            {
                throw new InvalidOperationException($"Handler is not set to handle messages of type {typeof(TRequest)}.");
            }

            Func<TRequest, Task<TResponse>> next = async (req) =>
            {
                return await handler.HandleAsync(req);
            };

            foreach (var middleware in middlewares)
            {
                next = (req) => middleware.ProcessAsync(req, next);
            }

            return await next(request);
        }
    }
}

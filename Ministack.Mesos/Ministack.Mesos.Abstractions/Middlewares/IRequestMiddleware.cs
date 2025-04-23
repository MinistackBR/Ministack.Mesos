using Ministack.Mesos.Abstractions.Data;
using System;
using System.Threading.Tasks;

namespace Ministack.Mesos.Abstractions.Middlewares
{
    public interface IRequestMiddleware<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : class
    {
        Task<TResponse> ProcessAsync(TRequest request, Func<TRequest, Task<TResponse>> next);
    }
}

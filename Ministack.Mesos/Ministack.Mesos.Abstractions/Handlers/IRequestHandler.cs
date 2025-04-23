using Ministack.Mesos.Abstractions.Data;
using System.Threading.Tasks;

namespace Ministack.Mesos.Abstractions.Handlers
{
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : class
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}

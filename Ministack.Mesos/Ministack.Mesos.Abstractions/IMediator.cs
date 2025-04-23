using Ministack.Mesos.Abstractions.Data;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Ministack.Mesos.Abstractions
{
    public interface IMediator
    {
        Task NotifyAsync<TNotification>(TNotification notification) where TNotification : INotification;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse> where TResponse : class;
    }
}

using MediatR;

namespace newApp.Infrastructure.Common
{
    public abstract class BaseQuery<TResponse> : IRequest<TResponse>
    {
    }
}
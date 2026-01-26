using MediatR;

namespace newApp.Infrastructure.Common
{
    public abstract class BaseCommand : IRequest
    {
    }

    public abstract class BaseCommand<TResponse> : IRequest<TResponse>
    {
    }
}
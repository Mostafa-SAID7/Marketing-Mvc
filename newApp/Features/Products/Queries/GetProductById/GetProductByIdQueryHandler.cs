using MediatR;
using newApp.Data;
using newApp.Models.entity;

namespace newApp.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Products.GetByIdAsync(request.Id);
        }
    }
}
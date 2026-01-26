using MediatR;
using newApp.Models.entity;

namespace newApp.Features.Products.Queries.GetProduct
{
    public class GetProductQuery : IRequest<Product?>
    {
        public Guid Id { get; set; }

        public GetProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
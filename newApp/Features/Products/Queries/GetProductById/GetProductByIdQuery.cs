using newApp.Infrastructure.Common;
using newApp.Models.entity;

namespace newApp.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : BaseQuery<Product?>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
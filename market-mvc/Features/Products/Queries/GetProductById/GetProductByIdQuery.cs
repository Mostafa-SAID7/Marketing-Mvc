using market_mvc.Infrastructure.Common;
using market_mvc.Models.entity;

namespace market_mvc.Features.Products.Queries.GetProductById
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

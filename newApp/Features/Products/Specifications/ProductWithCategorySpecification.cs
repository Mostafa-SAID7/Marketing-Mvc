using newApp.Infrastructure.Common;
using newApp.Models.entity;

namespace newApp.Features.Products.Specifications
{
    public class ProductWithCategorySpecification : BaseSpecification<Product>
    {
        public ProductWithCategorySpecification() : base()
        {
            AddInclude(p => p.Category);
        }

        public ProductWithCategorySpecification(Guid productId) : base(p => p.Id == productId)
        {
            AddInclude(p => p.Category);
        }
    }
}
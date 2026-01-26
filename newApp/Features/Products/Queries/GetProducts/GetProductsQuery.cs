using newApp.Infrastructure.Common;
using newApp.Models;

namespace newApp.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : BaseQuery<PaginatedResult<Models.entity.Product>>
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public string? PriceRange { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
using newApp.Infrastructure.Common;

namespace newApp.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : BaseCommand<bool>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
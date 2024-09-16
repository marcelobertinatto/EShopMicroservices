using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category,
        string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsUpdated);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(cmd => cmd.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(cmd => cmd.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characteres.");
            RuleFor(cmd => cmd.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session,
        ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, 
            CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);
                var product = await session.LoadAsync<Product>(command.Id, 
                    cancellationToken);

                if (product == null)
                {
                    throw new ProductNotFoundException(command.Id);
                }

                product.Name = command.Name;
                product.Category = command.Category;
                product.Description = command.Description;
                product.ImageFile = command.ImageFile;
                product.Price = command.Price;

                session.Update(product);
                await session.SaveChangesAsync(cancellationToken);

                return new UpdateProductResult(true);
            }
            catch (Exception ex)
            {
                throw new ProductNotFoundException(command.Id);
            }
        }
    }
}

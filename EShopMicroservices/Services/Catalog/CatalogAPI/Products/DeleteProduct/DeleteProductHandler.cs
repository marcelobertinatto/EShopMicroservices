namespace CatalogAPI.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
	public record DeleteProductResult(bool IsDeleted);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        }
    }

    internal class DeleteProductCommandHandler(IDocumentSession session,
        ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

                var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

                if (product == null)
                {
                    throw new ProductNotFoundException(command.Id);
                }

                session.Delete(product);
                await session.SaveChangesAsync();

                return new DeleteProductResult(true);
            }
            catch (Exception ex)
            {
                throw new ProductNotFoundException(command.Id);
            }
        }
    }
}


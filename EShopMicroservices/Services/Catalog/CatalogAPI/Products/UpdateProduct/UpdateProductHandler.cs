﻿using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category,
        string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);
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
                    throw new ProductNotFoundException();
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
                throw new ProductNotFoundException();
            }
        }
    }
}

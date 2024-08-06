﻿using MediatR;

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string name, List<string>Category, 
        string Description, string ImageFile, decimal Price) : IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // business logic to create a product
            throw new NotImplementedException();
        }
    }
}
﻿using Marten.Linq.QueryHandlers;

namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, 
            CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(x => x.Category.Contains(query.Category))
                .ToListAsync();

            return new GetProductsByCategoryResult(products);
        }
    }
}

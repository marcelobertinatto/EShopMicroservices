using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;

namespace Order.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers {get;set;}
        DbSet<Product> Products {get;set;}
        DbSet<Domain.Models.Order> Orders {get;set;}
        DbSet<OrderItem> OrderItems {get;set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
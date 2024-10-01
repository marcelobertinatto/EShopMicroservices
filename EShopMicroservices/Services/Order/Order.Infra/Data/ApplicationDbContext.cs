using System.Reflection;
using Order.Application.Data;

namespace Order.Infra.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Domain.Models.Order> Orders => Set<Domain.Models.Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        DbSet<Customer> IApplicationDbContext.Customers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DbSet<Product> IApplicationDbContext.Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DbSet<Domain.Models.Order> IApplicationDbContext.Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DbSet<OrderItem> IApplicationDbContext.OrderItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}

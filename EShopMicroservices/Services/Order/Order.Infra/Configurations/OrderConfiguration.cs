using Order.Domain.Enums;

namespace Order.Infra.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Models.Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.OrderItems)
                .WithOne()
                .HasForeignKey<OrderItem>(x => x.OrderId);

            builder.ComplexProperty(x => x.OrderName, nameBuilder =>
            {
                nameBuilder.Property(z => z.Value)
                .HasColumnName(nameof(OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(x => x.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(z => z.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.Zipcode)
                .HasMaxLength(5);
            });

            builder.ComplexProperty(x => x.BillingAddress, billingBuilder =>
            {
                billingBuilder.Property(z => z.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                billingBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                billingBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                billingBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                billingBuilder.Property(a => a.Country)
                .HasMaxLength(50);

                billingBuilder.Property(a => a.State)
                .HasMaxLength(50);

                billingBuilder.Property(a => a.Zipcode)
                .HasMaxLength(5);
            });

            builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(z => z.CardName)
                .HasMaxLength(50);

                paymentBuilder.Property(z => z.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                paymentBuilder.Property(x => x.Expiration)
                .HasMaxLength(10);

                paymentBuilder.Property(x => x.CVV)
                .HasMaxLength(3);

                paymentBuilder.Property(x => x.PaymentMethod);
            });

            builder.Property(x => x.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(x => x.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(x => x.TotalPrice);
        }
    }
}

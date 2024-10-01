using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Order.Infra.Data.Extensions
{
    public class InitialData
    {
        public static IEnumerable<Customer> Customers => 
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("0d69f8d2-a19e-4ffc-9e84-f4f53fbba747")),"Marcelo", "mbertinatto@hotmail.com"),
            Customer.Create(CustomerId.Of(new Guid("f3b64f34-97cb-4c02-864f-a47c94e8b070")),"Joao", "joao@hotmail.com")
        };

        public static IEnumerable<Product> Products =>
        new List<Product> 
        {
            Product.Create(ProductId.Of(new Guid("39ccfd6f-41f6-446c-aa5f-35e816881a8c")),"iPhone X", 500),
            Product.Create(ProductId.Of(new Guid("9299a4e8-9c13-4eab-8903-9ba301e0335d")),"Samsung 10", 400),
            Product.Create(ProductId.Of(new Guid("269edbe9-f4e4-4e05-b2cb-f425c4d09901")),"Huawei Plus", 650),
            Product.Create(ProductId.Of(new Guid("6958c67f-ba56-45a6-b6ba-9145d05ff351")),"Xiaomi Mi", 450)
        };

        public static IEnumerable<Domain.Models.Order> OrdersWithItems
        {
            get 
            {
                var address1 = Address.Of("Marcelo","Bertinatto","mbertinatto@hotmail.com","rua test", "Brasil", "SP", "12242");
                var address2 = Address.Of("Joao","Bertinatto","joao@hotmail.com","rua test", "Brasil", "SP", "12341");

                var payment1 = Payment.Of("Marcelo","1234567890124444","12/28","355",1);
                var payment2 = Payment.Of("Joao","1222567890124444","06/30","222",2);

                var order1 = Domain.Models.Order.Create(OrderId.Of(Guid.NewGuid()),
                        CustomerId.Of(new Guid("0d69f8d2-a19e-4ffc-9e84-f4f53fbba747")),
                        OrderName.Of("ORD_1"),
                        shippingAddress: address1,
                        billingAddress: address1,
                        payment1);

                order1.Add(ProductId.Of(new Guid("39ccfd6f-41f6-446c-aa5f-35e816881a8c")),2,500);
                order1.Add(ProductId.Of(new Guid("9299a4e8-9c13-4eab-8903-9ba301e0335d")),1,400);

                var order2 = Domain.Models.Order.Create(OrderId.Of(Guid.NewGuid()),
                        CustomerId.Of(new Guid("f3b64f34-97cb-4c02-864f-a47c94e8b070")),
                        OrderName.Of("ORD_2"),
                        shippingAddress: address2,
                        billingAddress: address2,
                        payment2);

                order2.Add(ProductId.Of(new Guid("39ccfd6f-41f6-446c-aa5f-35e816881a8c")),1,650);
                order2.Add(ProductId.Of(new Guid("9299a4e8-9c13-4eab-8903-9ba301e0335d")),2,450);

                return new List<Domain.Models.Order> {order1,order2};
            }
        }
    }
}
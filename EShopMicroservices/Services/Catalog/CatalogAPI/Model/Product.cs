namespace CatalogAPI.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string MyProperty { get; set; } = default!;
        public string Price { get; set; }
    }
}

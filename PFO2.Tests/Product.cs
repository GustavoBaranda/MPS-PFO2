
namespace PFO2.Tests
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }

        public Product(int id, string name, decimal price, Category category)
        {
            if (id <= 0)
                throw new ArgumentException("The ID cannot be negative", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The name cannot be empty", nameof(name));

            if (price <= 0)
                throw new ArgumentException("The price cannot be negative", nameof(price));
           
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}


namespace PFO2.Tests
{
    public interface IProductManager
    {
        List<Product> Products { get; set; }
        public Boolean AddProduct(Product product);

        public decimal CalculateTotalPrice();

        public decimal CalculateTotalPrice(Category category);
        public Product FindProductByName (string name);
    }
}

namespace PFO2.Tests
{
    public class ProductManager : IProductManager
    {
        public List<Product> Products { get; set; }

        public ProductManager()
        {
            this.Products = [];
        }

        public bool AddProduct(Product product)
        {
            if (product.Price < 0) return false;
            if (product.Name == null) return false;
            if (product.Category != Category.electronics && product.Category != Category.food) return false;
            Products.Add(product);
            return true;
        }

        public decimal CalculateTotalPrice()
        {
            decimal precioTotal = 0;
            foreach (Product product in Products)
            {
                precioTotal +=
                    product.Category == Category.electronics
                    ? product.Price * (1 + 0.10m)
                    : product.Price * (1 + 0.05m);
            }
            return precioTotal;
        }

        public decimal CalculateTotalPrice(Category category)
        {
            decimal precioTotal = 0;
            decimal porcentaje = (category == Category.food) ? 0.05m : 0.10m;

            foreach (Product product in Products)
            {
                if (product.Category == category)
                {
                    precioTotal += product.Price * (1 + porcentaje);
                }
            }

            return precioTotal;
        }

        public Product FindProductByName(string name)
        {
            var product = Products.Find(p => 
                p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) 
                ?? throw new Exception("Product not found");
            return product;
        }

    }
}

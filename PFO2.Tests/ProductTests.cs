
namespace PFO2.Tests
{
    public class ProductTests
    {
        private IProductManager _productManager;
        [SetUp]
        public void Setup()
        {
            _productManager = new ProductManager();
        }

        [Test]
        public void Test_CreateProduct_With_Valid_Data()
        {
            // Arrange
            int expectedId = 1;
            string expectedName = "Headphones";
            decimal expectedPrice = 499.00m;
            Category expectedCategory = Category.electronics;

            // Act
            Product product = new(expectedId, expectedName, expectedPrice, expectedCategory);

            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(product.Id, Is.EqualTo(expectedId));
                Assert.That(product.Name, Is.EqualTo(expectedName));
                Assert.That(product.Price, Is.EqualTo(expectedPrice));
                Assert.That(product.Category, Is.EqualTo(expectedCategory));
            });
            // Act & Assert - Invalid price
            var priceException = Assert.Throws<ArgumentException>(() =>
            {
                new Product(2, "Laptop", -35.00m, Category.electronics);
            });
            Assert.That(priceException.Message, Is.EqualTo("The price cannot be negative (Parameter 'price')"));

            // Act & Assert - Empty name
            var emptyNameException = Assert.Throws<ArgumentException>(() =>
            {
                new Product(3, "", 12.00m, Category.electronics);
            });
            Assert.That(emptyNameException.Message, Is.EqualTo("The name cannot be empty (Parameter 'name')"));

            // Act & Assert - Null or default name
            var defaultNameException = Assert.Throws<ArgumentException>(() =>
            {
                new Product(4, default!, 12.00m, Category.electronics);
            });
            Assert.That(defaultNameException.Message, Is.EqualTo("The name cannot be empty (Parameter 'name')"));

            // Act & Assert - Negative ID
            var negativeIdException = Assert.Throws<ArgumentException>(() =>
            {
                new Product(-1, "Keyboard", 9.00m, Category.electronics);
            });
            Assert.That(negativeIdException.Message, Is.EqualTo("The ID cannot be negative (Parameter 'id')"));
        }

        [Test]
        public void Test_AddProduct()
        {
            Product product = new(1, "Headphones", 499.00m, Category.electronics);
            int productQuantity = _productManager.Products.Count;
            int quantityExpected = 0;
            Assert.That(productQuantity, Is.EqualTo(quantityExpected));
            Boolean response = _productManager.AddProduct(product);
            Boolean expected = true;
            Assert.That(response, Is.EqualTo(expected));
            productQuantity = _productManager.Products.Count;
            quantityExpected = 1;
            Assert.That(productQuantity, Is.EqualTo(quantityExpected));
        }

        [Test]
        public void Test_Find_Product_By_Name()
        {
            List<Product> products =
            [
                new Product(1,"Headphones",499.00m,Category.electronics),
                new Product(2,"Pasta",9.00m,Category.food),
                new Product(3,"Keyboard",35.00m,Category.electronics),
                new Product(4,"Eggs",1.00m,Category.food),
                new Product(5,"Hamburger",12.00m,Category.food),
                new Product(6,"mouse",19.00m,Category.electronics),
            ];
            products.ForEach(p => _productManager.AddProduct(p));

            // Test 1
            Product product1 = _productManager.FindProductByName("Pasta");
            Assert.Multiple(() =>
            {
                Assert.That(product1.Name, Is.EqualTo("Pasta"));
                Assert.That(product1.Id, Is.EqualTo(2));
                Assert.That(product1.Category, Is.EqualTo(Category.food));
            });

            // Test 2
            Product product2 = _productManager.FindProductByName("mouse");
            Assert.Multiple(() =>
            {
                Assert.That(product2.Name, Is.EqualTo("mouse"));
                Assert.That(product2.Category, Is.EqualTo(Category.electronics));
                Assert.That(product2.Price, Is.EqualTo(19.00m));
            });


            // Test 3
            Product product3 = _productManager.FindProductByName("Keyboard");
            Assert.Multiple(() =>
            {
                Assert.That(product3.Name, Is.EqualTo("Keyboard"));
                Assert.That(product3.Category, Is.EqualTo(Category.electronics));
                Assert.That(product3.Price, Is.Not.EqualTo(29.00m));
            });

            // Test 4
           var exception = Assert.Throws<Exception>(() => 
                _productManager.FindProductByName("Monitor"), 
                "An exception should be thrown if the product is not found.");

            Assert.That(exception.Message, Is.EqualTo("Product not found"));
        }

        [Test]
        public void Test_Calculate_Total_Price_electronics()
        {
            List<Product> products =
            [
                new Product(1,"Laptop",499.00m,Category.electronics),
                new Product(2,"Pasta",9.00m,Category.food),
                new Product(3,"Keyboard",35.00m,Category.electronics),
                new Product(4,"Eggs",1.00m,Category.food),
                new Product(5,"Hamburger",12.00m,Category.food),
                new Product(6,"mouse",19.00m,Category.electronics),
            ];
            products.ForEach(p => _productManager.AddProduct(p));

            decimal totalExpected = 608.3m;
            decimal totalPriceCalculated = _productManager.CalculateTotalPrice(Category.electronics);

            Assert.That(totalPriceCalculated, Is.EqualTo(totalExpected));
        }

        [Test]
        public void Test_Calculate_Total_Price_food()
        {
            List<Product> products =
            [
            new Product(1,"Laptop",499.00m,Category.electronics),
            new Product(2,"Pasta",9.00m,Category.food),
            new Product(3,"Keyboard",35.00m,Category.electronics),
            new Product(4,"Eggs",1.00m,Category.food),
            new Product(5,"Hamburger",12.00m,Category.food),
            new Product(6,"mouse",19.00m,Category.electronics),
            ];
            products.ForEach(p => _productManager.AddProduct(p));

            decimal totalExpected = 23.1m;
            decimal totalPriceCalculated = _productManager.CalculateTotalPrice(Category.food);

            Assert.That(totalPriceCalculated, Is.EqualTo(totalExpected));
        }

        [Test]
        public void Test_Calculate_Total_Price()
        {
            List<Product> products =
            [
            new Product(1,"Laptop",499.00m,Category.electronics),
            new Product(2,"Pasta",9.00m,Category.food),
            new Product(3,"Keyboard",35.00m,Category.electronics),
            new Product(4,"Eggs",1.00m,Category.food),
            new Product(5,"Hamburger",12.00m,Category.food),
            new Product(6,"mouse",19.00m,Category.electronics),
            ];
            products.ForEach(p => _productManager.AddProduct(p));


            decimal totalExpectedfood = 23.1m;
            decimal totalExpectedelectronics = 608.3m;
            decimal totalExpected = totalExpectedfood + totalExpectedelectronics;

            decimal totalPriceCalculated = _productManager.CalculateTotalPrice();
            decimal totalPriceCalculatedfood = _productManager.CalculateTotalPrice(Category.food);
            decimal totalPriceCalculatedelectronics = _productManager.CalculateTotalPrice(Category.electronics);

            Assert.Multiple(() =>
            {
                Assert.That(totalPriceCalculated, Is.EqualTo(totalExpected));
                Assert.That(totalPriceCalculatedfood, Is.EqualTo(totalExpectedfood));
                Assert.That(totalPriceCalculatedelectronics, Is.EqualTo(totalExpectedelectronics));
            });
        }
    }
}
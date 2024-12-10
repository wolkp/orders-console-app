namespace OrderApp
{
    public class Program
    {
        public static void Main()
        {
            var products = InitializeProducts();
            var discounts = InitializeDiscounts();
            var order = new Order(discounts.Item1, discounts.Item2);

            var isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                DisplayAvailableProducts(products);
                DisplayCurrentOrder(order);
                ShowMenu();

                var option = Console.ReadLine()?.ToLower();
                isRunning = HandleMenuOption(option, order, products);
            }

            Console.WriteLine("Exiting the application.");
        }

        private static List<Product> InitializeProducts()
        {
            return new List<Product>
            {
                new Product(ProductConstants.Laptop, 2500),
                new Product(ProductConstants.Keyboard, 120),
                new Product(ProductConstants.Mouse, 90),
                new Product(ProductConstants.Monitor, 1000),
                new Product(ProductConstants.Duck, 66)
            };
        }

        private static Tuple<ExclusiveDiscounts, List<Discount>> InitializeDiscounts()
        {
            var secondCheapestDiscount = new SecondCheapestProductDiscount(0.10m);
            var thirdCheapestDiscount = new ThirdCheapestProductDiscount(0.20m);
            var totalOrderDiscount = new TotalOrderDiscount(0.05m, 5000);

            var exclusiveDiscounts = new ExclusiveDiscounts(new List<Discount>
            {
                secondCheapestDiscount,
                thirdCheapestDiscount
            });

            var globalDiscounts = new List<Discount> { totalOrderDiscount };

            return new Tuple<ExclusiveDiscounts, List<Discount>>(exclusiveDiscounts, globalDiscounts);
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine($"{MenuConstants.AddProductOption}. Add Product");
            Console.WriteLine($"{MenuConstants.RemoveProductOption}. Remove Product");
            Console.WriteLine($"{MenuConstants.DisplayOrderValueOption}. Display Order Value");
            Console.WriteLine($"{MenuConstants.ExitOption}. Exit");
            Console.Write($"Select an option " +
                $"({MenuConstants.AddProductOption}, {MenuConstants.RemoveProductOption}, {MenuConstants.DisplayOrderValueOption}, {MenuConstants.ExitOption}): ");
        }

        private static bool HandleMenuOption(string? option, Order order, List<Product> products)
        {
            switch (option)
            {
                case MenuConstants.AddProductOption:
                    AddProductToOrder(order, products);
                    break;
                case MenuConstants.RemoveProductOption:
                    RemoveProductFromOrder(order);
                    break;
                case MenuConstants.DisplayOrderValueOption:
                    DisplayOrderValue(order);
                    break;
                case MenuConstants.ExitOption:
                    return false;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            return true;
        }

        private static void DisplayAvailableProducts(List<Product> products)
        {
            Console.WriteLine("Available Products:");
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                Console.WriteLine($"{i + 1}. {product.Name} - {product.Price} PLN");
            }
            Console.WriteLine();
        }

        private static void DisplayCurrentOrder(Order order)
        {
            Console.WriteLine("Current order:");

            if(order.Items.Any())
            {
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"- {item.Product.Name} x{item.Quantity} ({item.Product.Price} PLN each)");
                }
            }
            else
            {
                Console.WriteLine("Order is empty.");
            }
            Console.WriteLine();
        }

        private static void AddProductToOrder(Order order, List<Product> products)
        {
            Console.Write("Enter the product name to add: ");
            var productName = Console.ReadLine();

            var product = products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {
                Console.Write("Enter the quantity to add: ");
                if (int.TryParse(Console.ReadLine(), out var quantity) && quantity > 0)
                {
                    order.AddProduct(product, quantity);
                    Console.WriteLine($"{quantity} of {product.Name} added to the order.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity.");
                }
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private static void RemoveProductFromOrder(Order order)
        {
            Console.Write("Enter the product name to remove: ");
            var productName = Console.ReadLine();

            var product = order.GetProductByName(productName);
            if (product != null)
            {
                Console.Write("Enter the quantity to remove: ");
                if (int.TryParse(Console.ReadLine(), out var quantity))
                {
                    var currentQuantity = order.GetProductQuantity(product);
                    var quantityToRemove = Math.Min(quantity, currentQuantity);

                    order.RemoveProduct(product, quantityToRemove);
                    Console.WriteLine($"Removed {quantityToRemove} of {product.Name}.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity.");
                }
            }
            else
            {
                Console.WriteLine("Product not found in the order.");
            }
        }

        private static void DisplayOrderValue(Order order)
        {
            var totalPrice = CalculateOrderTotalPrice(order);
            Console.WriteLine($"Total order value after discounts: {totalPrice} PLN");
        }

        private static decimal CalculateOrderTotalPrice(Order order)
        {
            return order.CalculateTotalPrice();
        }
    }
}

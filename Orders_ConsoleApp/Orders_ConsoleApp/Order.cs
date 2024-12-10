namespace OrderApp
{ 
    public class Order
    {
        private readonly List<OrderItem> _items = new();
        private readonly ProductComparer _productComparer = new();
        private readonly List<Discount> _globalDiscounts;
        private readonly ExclusiveDiscounts _exclusiveDiscounts;

        public Order(ExclusiveDiscounts exclusiveDiscounts, List<Discount> globalDiscounts)
        {
            _exclusiveDiscounts = exclusiveDiscounts;
            _globalDiscounts = globalDiscounts;
        }

        public void AddProduct(Product addedProduct, int quantity)
        {
            if (addedProduct == null)
                throw new ArgumentNullException(nameof(addedProduct), "Added product cannot be null.");

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            var existingItem = _items.FirstOrDefault(i => _productComparer.Equals(i.Product, addedProduct));

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                var newOrderItem = new OrderItem(addedProduct, quantity);
                _items.Add(newOrderItem);
            }
        }

        public void RemoveProduct(Product removedProduct, int quantity)
        {
            if (removedProduct == null)
                throw new ArgumentNullException(nameof(removedProduct), "Removed product cannot be null.");

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

            var existingItem = _items.FirstOrDefault(i => _productComparer.Equals(i.Product, removedProduct));

            if (existingItem == null)
                throw new InvalidOperationException("Product not found in the order.");

            if (quantity >= existingItem.Quantity)
            {
                _items.Remove(existingItem);
            }
            else
            {
                existingItem.DecreaseQuantity(quantity);
            }
        }

        public decimal CalculateTotalPrice()
        {
            var totalPrice = _items.Sum(i => i.GetTotalPrice());
            var totalPriceDiscounted = ApplyDiscounts(totalPrice);

            return totalPriceDiscounted;
        }

        public void DisplayOrder()
        {
            Console.WriteLine("Order details:"); 

            foreach (var item in _items)
            {
                Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Quantity}, Total: {item.Product.Price * item.Quantity} PLN");
            }
        }

        private decimal ApplyDiscounts(decimal totalPrice)
        {
            var priceAfterExclusiveDiscounts = ApplyExclusiveDiscounts(totalPrice);
            var priceAfterGlobalDiscounts = ApplyGlobalDiscounts(priceAfterExclusiveDiscounts);

            return priceAfterGlobalDiscounts;
        }

        private decimal ApplyExclusiveDiscounts(decimal totalPrice)
        {
            totalPrice = _exclusiveDiscounts.ApplyBestDiscount(_items, totalPrice);

            return totalPrice;
        }

        private decimal ApplyGlobalDiscounts(decimal totalPrice)
        {
            foreach (var globalDiscount in _globalDiscounts)
            {
                totalPrice = globalDiscount.Apply(_items, totalPrice);
            }

            return totalPrice;
        }
    }
}

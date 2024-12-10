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

        public void AddItem(OrderItem addedItem)
        {
            if (addedItem == null)
                throw new ArgumentNullException(nameof(addedItem), "Added item cannot be null.");

            var existingItem = _items.FirstOrDefault(i => _productComparer.Equals(i.Product, addedItem.Product));

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(addedItem.Quantity);
            }
            else
            {
                _items.Add(addedItem);
            }
        }

        public void RemoveItem(OrderItem removedItem)
        {
            if (removedItem == null)
                throw new ArgumentNullException(nameof(removedItem), "Removed item cannot be null.");

            _items.Remove(removedItem);
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

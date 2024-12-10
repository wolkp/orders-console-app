﻿namespace OrderApp
{ 
    public class Order
    {
        private readonly List<OrderItem> _items = new();
        private readonly ProductComparer _productComparer = new();
        private readonly List<Discount> _discounts;

        public Order(List<Discount> discounts)
        {
            _discounts = discounts;
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

        private decimal ApplyDiscounts(decimal totalPrice)
        {
            foreach(var discount in _discounts)
            {
                totalPrice = discount.Apply(_items, totalPrice);
            }

            return totalPrice;
        }
    }
}

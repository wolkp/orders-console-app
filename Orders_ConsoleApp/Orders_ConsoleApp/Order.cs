﻿namespace OrderApp
{ 
    public class Order
    {
        private readonly List<OrderItem> _items = new();

        public void AddItem(OrderItem addedItem)
        {
            if (addedItem == null)
                throw new ArgumentNullException(nameof(addedItem), "Added item cannot be null.");

            var existingItem = _items.FirstOrDefault(i => i.Product.Name == i.Product.Name);

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
            return totalPrice;
        }
    }
}

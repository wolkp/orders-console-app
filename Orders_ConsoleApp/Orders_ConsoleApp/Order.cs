namespace OrderApp
{ 
    public class Order
    {
        private readonly List<OrderItem> _items = new();

        public void AddProduct(Product product, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.Product.Name == product.Name);

            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new OrderItem(product, quantity);
                _items.Add(newItem);
            }
        }

        public void RemoveProduct(string productName)
        {
            var itemToRemove = _items.FirstOrDefault(i => i.Product.Name == productName);

            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
            }
        }

        public decimal CalculateTotalPrice()
        {
            var totalPrice = _items.Sum(i => i.GetTotalPrice());
            return totalPrice;
        }
    }
}

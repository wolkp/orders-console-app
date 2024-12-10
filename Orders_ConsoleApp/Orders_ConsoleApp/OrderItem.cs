namespace OrderApp
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; private set; }

        public OrderItem(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.");
        }

        public decimal GetTotalPrice()
        {
            return Product.Price * Quantity;
        }

        public void IncreaseQuantity(int increaseAmount)
        {
            if (increaseAmount <= 0)
                throw new ArgumentException("Quantity to increase must be greater than zero.");

            Quantity += increaseAmount;
        }

        public void DecreaseQuantity(int decreaseAmount)
        {
            if (decreaseAmount <= 0)
                throw new ArgumentException("Quantity to decrease must be greater than zero.");

            if (Quantity - decreaseAmount < 0)
                throw new InvalidOperationException("Cannot decrease quantity below zero.");

            Quantity -= decreaseAmount;
        }
    }
}
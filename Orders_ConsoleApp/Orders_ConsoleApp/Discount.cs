namespace OrderApp
{
    public abstract class Discount
    {
        public decimal DiscountPercentage { get; }

        public abstract decimal Apply(List<OrderItem> items, decimal totalPrice);

        public Discount(decimal discountPercentage)
        {
            DiscountPercentage = discountPercentage;
        }

        protected decimal GetDiscountedPrice(decimal price)
        {
            var discountedPrice = price * DiscountPercentage;

            return discountedPrice;
        }
    }
}
namespace OrderApp
{
    public class TotalOrderDiscount : Discount
    {
        private readonly decimal _threshold;

        public TotalOrderDiscount(decimal discountPercentage, decimal threshold)
            : base(discountPercentage)
        {
            _threshold = threshold;
        }

        public override decimal Apply(List<OrderItem> items, decimal totalPrice)
        {
            if (totalPrice > _threshold)
            {
                var discount = GetDiscountedPrice(totalPrice);
                totalPrice -= discount;
            }

            return totalPrice;
        }
    }
}
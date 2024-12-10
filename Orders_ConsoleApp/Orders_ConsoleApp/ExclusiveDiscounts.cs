namespace OrderApp
{
    public class ExclusiveDiscounts
    {
        private readonly List<Discount> _discounts;

        public ExclusiveDiscounts(List<Discount> discounts)
        {
            _discounts = discounts;
        }

        public decimal ApplyBestDiscount(List<OrderItem> items, decimal totalPrice)
        {
            var bestPrice = totalPrice;

            foreach (var discount in _discounts)
            {
                var priceAfterDiscount = discount.Apply(items, totalPrice);

                if (priceAfterDiscount < bestPrice)
                {
                    bestPrice = priceAfterDiscount;
                }
            }

            return bestPrice;
        }
    }
}
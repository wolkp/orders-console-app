namespace OrderApp
{
    public class ThirdCheapestProductDiscount : Discount
    {
        public ThirdCheapestProductDiscount(decimal discountPercentage) : base(discountPercentage) { }

        public override decimal Apply(List<OrderItem> items, decimal totalPrice)
        {
            var sortedItems = items.OrderBy(item => item.Product.Price).ToList();

            if (sortedItems.Count >= 3)
            {
                var thirdCheapestItem = sortedItems[2];
                var discount = GetDiscountedPrice(thirdCheapestItem.Product.Price);
                totalPrice -= discount;
            }

            return totalPrice;
        }
    }
}
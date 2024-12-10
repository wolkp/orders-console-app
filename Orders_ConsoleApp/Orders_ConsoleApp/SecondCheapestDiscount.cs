namespace OrderApp
{
    public class SecondCheapestProductDiscount : Discount
    {
        public SecondCheapestProductDiscount(decimal discountPercentage) : base(discountPercentage) { }

        public override decimal Apply(List<OrderItem> items, decimal totalPrice)
        {
            var sortedItems = items.OrderBy(item => item.Product.Price).ToList();

            if (sortedItems.Count >= 2)
            {
                var secondCheapestItem = sortedItems[1];
                var discount = GetDiscountedPrice(secondCheapestItem.Product.Price);
                totalPrice -= discount;
            }

            return totalPrice;
        }
    }
}
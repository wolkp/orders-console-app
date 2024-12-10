namespace OrderApp
{
    public class ThirdCheapestProductDiscount : Discount
    {
        public ThirdCheapestProductDiscount(decimal discountPercentage) : base(discountPercentage) { }

        public override decimal Apply(List<OrderItem> items, decimal totalPrice)
        {
            var expandedList = new List<OrderItem>();
            foreach (var item in items)
            {
                for (var i = 0; i < item.Quantity; i++)
                {
                    expandedList.Add(item);
                }
            }

            var sortedItems = expandedList.OrderBy(item => item.Product.Price).ToList();

            if (sortedItems.Count >= 3)
            {
                var thirdCheapestItem = sortedItems.First();
                var discount = GetDiscountedPrice(thirdCheapestItem.Product.Price);
                totalPrice -= discount;
            }

            return totalPrice;
        }
    }
}
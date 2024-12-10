namespace OrderApp
{
    public class SecondCheapestProductDiscount : Discount
    {
        public SecondCheapestProductDiscount(decimal discountPercentage) : base(discountPercentage) { }

        public override decimal Apply(List<OrderItem> items, decimal totalPrice)
        {
            var expandedList = new List<OrderItem>();
            foreach(var item in items)
            {
                for(var i = 0; i < item.Quantity; i++)
                {
                    expandedList.Add(item);
                }
            }

            var sortedItems = expandedList.OrderBy(item => item.Product.Price).ToList();

            if (sortedItems.Count == 2)
            {
                var secondCheapestItem = sortedItems.First();
                var discount = GetDiscountedPrice(secondCheapestItem.Product.Price);
                totalPrice -= discount;
            }

            return totalPrice;
        }
    }
}
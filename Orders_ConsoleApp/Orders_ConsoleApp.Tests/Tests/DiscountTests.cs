using System.Collections.Generic;
using System.Linq;

public class DiscountTests
{
    [Fact]
    public void SecondCheapestDiscount_ShouldApplyToSecondCheapestProduct()
    {
        var discount = new SecondCheapestProductDiscount(0.10m);
        var orderItems = new List<OrderItem>
        {
            new OrderItem(new Product(ProductConstants.Laptop, 2500), 1),
            new OrderItem(new Product(ProductConstants.Keyboard, 120), 1)
        };

        var totalPrice = GetTotalPrice(orderItems);
        var totalDiscountedPrice = discount.Apply(orderItems, totalPrice);
        var discountValue = totalPrice - totalDiscountedPrice;

        Assert.Equal(12, discountValue); // 10% of 120
    }

    [Fact]
    public void ThirdCheapestDiscount_ShouldApplyToThirdCheapestProduct()
    {
        var discount = new ThirdCheapestProductDiscount(0.20m);
        var orderItems = new List<OrderItem>
        {
            new OrderItem(new Product(ProductConstants.Laptop, 2500), 1),
            new OrderItem(new Product(ProductConstants.Mouse, 90), 1),
            new OrderItem(new Product(ProductConstants.Keyboard, 120), 1),
            new OrderItem(new Product(ProductConstants.Monitor, 1000), 1)
        };

        var totalPrice = GetTotalPrice(orderItems);
        var totalDiscountedPrice = discount.Apply(orderItems, totalPrice);
        var discountValue = totalPrice - totalDiscountedPrice;

        Assert.Equal(18, discountValue); // 20% of 90
    }

    [Fact]
    public void TotalOrderDiscount_ShouldApplyWhenThresholdIsMet()
    {
        var discount = new TotalOrderDiscount(0.05m, 5000);
        var orderItems = new List<OrderItem>
        {
            new OrderItem(new Product(ProductConstants.Laptop, 2500), 1),
            new OrderItem(new Product(ProductConstants.Monitor, 3000), 1),
        };

        var totalPrice = GetTotalPrice(orderItems);
        var totalDiscountedPrice = discount.Apply(orderItems, totalPrice);
        var discountValue = totalPrice - totalDiscountedPrice;

        Assert.Equal(275, discountValue); // 5% of 5500
    }

    [Fact]
    public void ExclusiveDiscounts_ShouldApplyOnlyTheBestDiscount()
    {
        var secondCheapest = new SecondCheapestProductDiscount(0.10m);
        var thirdCheapest = new ThirdCheapestProductDiscount(0.20m);
        var exclusiveDiscounts = new ExclusiveDiscounts(new List<Discount> { secondCheapest, thirdCheapest });
        var orderItems = new List<OrderItem>
        {
            new OrderItem(new Product(ProductConstants.Laptop, 2500), 1),
            new OrderItem(new Product(ProductConstants.Mouse, 90), 1),
            new OrderItem(new Product(ProductConstants.Keyboard, 120), 1),
            new OrderItem(new Product(ProductConstants.Monitor, 1000), 1)
        };

        var totalPrice = GetTotalPrice(orderItems);
        var totalDiscountedPrice = exclusiveDiscounts.ApplyBestDiscount(orderItems, GetTotalPrice(orderItems));
        var discountValue = totalPrice - totalDiscountedPrice;

        Assert.Equal(18, discountValue); // The best discount is 20% of 90
    }

    private decimal GetTotalPrice(List<OrderItem> orderItems)
    {
        var totalPrice = orderItems.Sum(o => o.GetTotalPrice());
        return totalPrice;
    }

}

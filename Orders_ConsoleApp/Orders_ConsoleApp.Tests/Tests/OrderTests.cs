using System.Collections.Generic;

public class OrderTests
{
    private static ExclusiveDiscounts GetDummyExclusiveDiscounts()
    {
        return new ExclusiveDiscounts(new List<Discount>());
    }

    private static List<Discount> GetDummyGlobalDiscounts()
    {
        return new List<Discount>();
    }

    private static Order CreateTestOrder()
    {
        return new Order(GetDummyExclusiveDiscounts(), GetDummyGlobalDiscounts());
    }

    [Fact]
    public void AddProduct_ShouldAddProductToOrder()
    {
        var product = new Product(ProductConstants.Laptop, 2500);
        var order = CreateTestOrder();

        order.AddProduct(product, 2);

        Assert.Single(order.Items);
        Assert.Equal(2, order.Items[0].Quantity);
        Assert.Equal(product, order.Items[0].Product);
    }

    [Fact]
    public void RemoveProduct_ShouldRemoveSpecifiedQuantity()
    {
        var product = new Product(ProductConstants.Laptop, 2500);
        var order = CreateTestOrder();
        order.AddProduct(product, 3);

        order.RemoveProduct(product, 2);

        Assert.Single(order.Items);
        Assert.Equal(1, order.Items[0].Quantity);
    }

    [Fact]
    public void RemoveProduct_ShouldRemoveAllWhenQuantityExceedsAvailable()
    {
        var product = new Product(ProductConstants.Laptop, 2500);
        var order = CreateTestOrder();
        order.AddProduct(product, 3);

        order.RemoveProduct(product, 5);

        Assert.Empty(order.Items);
    }

    [Fact]
    public void CalculateTotalPrice_ShouldReturnSumOfProductPrices()
    {
        var product1 = new Product(ProductConstants.Laptop, 2500);
        var product2 = new Product(ProductConstants.Mouse, 90);
        var order = CreateTestOrder();
        order.AddProduct(product1, 1);
        order.AddProduct(product2, 2);

        var totalPrice = order.CalculateTotalPrice();

        Assert.Equal(2680, totalPrice);
    }
}
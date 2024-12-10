public class OrderItemTests
{
    [Fact]
    public void OrderItem_ShouldInitializePropertiesCorrectly()
    {
        var product = new Product(ProductConstants.Laptop, 2500);

        var orderItem = new OrderItem(product, 3);

        Assert.Equal(product, orderItem.Product);
        Assert.Equal(3, orderItem.Quantity);
    }
}

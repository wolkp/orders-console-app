public class ProductTests
{
    [Fact]
    public void Product_ShouldInitializePropertiesCorrectly()
    {
        var product = new Product(ProductConstants.Laptop, 2500);

        Assert.Equal(ProductConstants.Laptop, product.Name);
        Assert.Equal(2500, product.Price);
    }
}

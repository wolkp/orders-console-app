namespace OrderApp
{
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x == null || y == null)
                return false;

            return x.Name == y.Name && x.Price == y.Price;
        }

        public int GetHashCode(Product obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return HashCode.Combine(obj.Name, obj.Price);
        }
    }
}
namespace Trendyol.Scripts
{
    public class Product
    {
        public string Name { get; private set; }

        public double Price { get; private set; }

        public Category Category { get; private set; }

        public Product(string name, double price, Category category)
        {
            Name = name;
            Price = price;
            Category = category;                  
        }


    }
}

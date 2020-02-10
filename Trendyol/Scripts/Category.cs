namespace Trendyol.Scripts
{
    public class Category
    {
        public string Name { get; private set; }

        public Category ParentCategory { get; private set; }

        public Category(string name)
        {
            Name = name;
            ParentCategory = null; 
        }

        public Category(string name, Category parent)
        {
            Name = name;
            ParentCategory = parent;
        }
    }
}

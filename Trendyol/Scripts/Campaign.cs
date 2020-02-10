using System.Collections.Generic;

namespace Trendyol.Scripts
{
    public class Campaign : Discount
    {
        public Category Category { get; private set; }
        public int MinQuantity { get; private set; }

        public Campaign(Category category, double discount, int quantitiy,
             DiscountType discountType)
        {
            Category = category;
            MinQuantity = quantitiy;
            DiscountAmount = discount;
            DiscountType = discountType;        
        }

        public override double ApplyDiscount(Dictionary<Product, int> cartProducts, double TotalAmount)
        {
            Category parentCategory;
            int currentQuantity = 0;
            double TotalCategoryPrice = 0;
           
            foreach (var product in cartProducts)
            {
                parentCategory = product.Key.Category.ParentCategory;
                //Search for any matches on parent
                while (parentCategory != null)
                {
                    if (parentCategory == Category)
                    {
                        currentQuantity += product.Value;
                        TotalCategoryPrice += product.Value * product.Key.Price;
                        break;
                    }
                    parentCategory = parentCategory.ParentCategory;
                }

                if (product.Key.Category == Category)
                {
                    currentQuantity += product.Value;
                    TotalCategoryPrice += product.Value * product.Key.Price;
                }          
            }

            if (currentQuantity < MinQuantity || TotalCategoryPrice <= 0) return 0;

            return CalculateDiscountByType(TotalCategoryPrice);                          
        } 
    }
}

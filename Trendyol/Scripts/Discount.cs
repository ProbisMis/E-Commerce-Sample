using System.Collections.Generic;

namespace Trendyol.Scripts
{
    public enum DiscountType
    {
        Amount,
        Rate
    }

    public abstract class Discount 
    {
        public double DiscountAmount { get; set; } //discount to be applied depends on type 

        public DiscountType DiscountType { get; set; }

        public abstract double ApplyDiscount(Dictionary<Product, int> cartProducts, double totalAmount);

        public virtual double CalculateDiscountByType(double TotalAmount)
        {

            if (DiscountType == DiscountType.Amount)
            {
                return DiscountAmount;
            }
            else
            {
                return (TotalAmount * DiscountAmount / 100);
            }
        }

        
    }
}

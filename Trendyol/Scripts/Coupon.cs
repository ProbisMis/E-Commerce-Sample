using System.Collections.Generic;

namespace Trendyol.Scripts
{
    public  class Coupon : Discount
    {
    
        public int MinimumBasketAmount { get; private set; }


        public Coupon(int minBasketTotal, double discount, DiscountType discountType)
        {
            MinimumBasketAmount = minBasketTotal;
            DiscountAmount = discount;
            DiscountType = discountType;
        }

        public override double ApplyDiscount(Dictionary<Product, int> cartProducts, double TotalAmount)
        {
            if (MinimumBasketAmount > TotalAmount || TotalAmount <= 0) return 0;

            return CalculateDiscountByType(TotalAmount);
        }

       


    }
}

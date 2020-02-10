using System.Collections.Generic;
using System;

namespace Trendyol.Scripts
{
    public class Cart
    {
        public int TotalQuantity { get; set; }
        public double TotalAmount { get; set; }

        public double CampaignDiscountAmount { get; set; }

        public double CouponDiscountAmount { get; set; }

        public double FinalAmount { get { return TotalAmount - (CampaignDiscountAmount + CouponDiscountAmount); } }

        public double DeliveryAmount { get; set; }

        private readonly Dictionary<Product, int> Products;

        public Cart()
        {
            Products = new Dictionary<Product, int>();
        }

        /// <summary>
        /// adds item to basket with given quantity
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity)
        {
            if (product == null || quantity <= 0) return;

            if (Products.ContainsKey(product))
            {
                Products[product] += quantity;
            }     
            else
            {
                Products.Add(product, quantity);
            }
            TotalQuantity += quantity;
            TotalAmount += product.Price * quantity;
        }

  
        /// <summary>
        /// print basket info
        /// </summary>
        public void Print()
        {
            HashSet<Category> categories = GetDistinctCategories();
            
          
            foreach (var category in categories)
            {
                List<Product> products = GetProductsByCategory(category);
                foreach (var product in products)
                {
                    Console.WriteLine("Category Name: " + category.Name + "," +
                        " Product Name: " + product.Name +
                        ", Quantity: " + Products[product] +
                        ", Unit Price: " + product.Price +
                        ", Total Price: " + ( Products[product] * product.Price) +
                        ", Total Discount applied: " + (CampaignDiscountAmount + CouponDiscountAmount)                     
                        );;
                }        
            }
            Console.WriteLine("Delivery Cost: " + DeliveryAmount +
                        ", Total Amount: " + FinalAmount);
        }

        /// <summary>
        /// returns list of products that belongs to a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Product> GetProductsByCategory(Category category)
        {
            
            List<Product> result = new List<Product>();
            
            foreach (var product in Products)
            {
                if (product.Key.Category == category)
                    result.Add(product.Key);
            }
            return result;
        }

        /// <summary>
        /// Finds largest amount of discount
        /// </summary>
        /// <param name="campaings"></param>
        public void ApplyDiscounts(params Campaign[] campaings )
        {
            //TODO: OCP Result: Bad SRP 
            if (campaings == null || CouponDiscountAmount > 0) return;

            double maxDiscountAmount = 0;
            double discountAmount;
            foreach (var campain in campaings)
            {
                discountAmount = campain.ApplyDiscount(Products, TotalAmount);
                if (discountAmount > maxDiscountAmount)
                {
                    maxDiscountAmount = discountAmount;
                    
                }
            }

            CampaignDiscountAmount =  maxDiscountAmount;
        }

        /// <summary>
        /// apply coupon  discount
        /// </summary>
        /// <param name="coupon"></param>
        public void ApplyDiscounts(Coupon coupon)
        {
            //TODO: OCP Result : Bad SRP
            if (coupon == null || CouponDiscountAmount > 0) return;

            double discoutAmount = coupon.ApplyDiscount(null, TotalAmount - CampaignDiscountAmount);

            CouponDiscountAmount = discoutAmount; 
        }

        /// <summary>
        /// Gets the number of different products in the basket
        /// </summary>
        public int GetNumberOfProducts()
        {
            return Products.Count;
        }
 
        /// <summary>
        /// Gets the number of distinct categories in the basket
        /// </summary>
        public int GetNumberOfDistinctCategories()
        {

            HashSet<Category> categories = GetDistinctCategories();
            return categories.Count;
        }

        /// <summary>
        /// Returns list of distinct categories 
        /// </summary>
        /// <returns></returns>
        public HashSet<Category> GetDistinctCategories()
        {
            HashSet<Category> categories = new HashSet<Category>();
            foreach (var product in Products)
            {
                categories.Add(product.Key.Category);
            }
            return categories;
        }

        /// <summary>
        /// Applied coupon discount
        /// </summary>
        public double GetCouponDiscount()
        {
            return CouponDiscountAmount;
        }

        /// <summary>
        /// Applied campaign discount
        /// </summary>
        public double GetCampaignDiscount()
        {
            return CampaignDiscountAmount;
        }

        /// <summary>
        /// Total delivery cost
        /// </summary>
        public double GetDeliveryCost()
        {
            return DeliveryAmount;
        }













    }
}

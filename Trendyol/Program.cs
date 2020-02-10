using System;
using Trendyol.Scripts;

namespace Trendyol
{
    class Program
    {
        static void Main(string[] args)
        {    
            Category meyve = new Category("meyve");
            Category sebze = new Category("sebze");

            Product elma = new Product("elma", 10, meyve);
            Product armut = new Product("armut", 10, meyve);
            Product saman = new Product("saman", 10, sebze);

            Cart basket = new Cart();
            basket.AddItem(elma, 10);
            basket.AddItem(saman, 10);
            basket.AddItem(armut, 10);

            //Console.WriteLine(meyve.Products.Count);
            //If more than 5 items on given category discount of 10
            Campaign campaign1 = new Campaign(meyve, 10.0, 5, DiscountType.Amount);
            Campaign campaign2 = new Campaign(sebze, 15.0, 2, DiscountType.Rate);
            Campaign campaign3 = new Campaign(meyve, 10.0, 50, DiscountType.Rate);


            basket.ApplyDiscounts(campaign1, campaign2, campaign3);

            Coupon coupon = new Coupon(100, 10, DiscountType.Amount);

            basket.ApplyDiscounts(coupon);

            //int x=  basket.GetNumberOfProducts();
            //Console.WriteLine("DISTINCT : " + x);
            //int y = basket.GetTotalNumberOfProducts();
            //Console.WriteLine("TOTAL : " + y);
            DeliveryCostCalculator dev = new DeliveryCostCalculator(2, 2, 3);
            dev.calculateFor(basket);


            basket.Print();




            Console.ReadLine(); //Pause
        }
    }
}

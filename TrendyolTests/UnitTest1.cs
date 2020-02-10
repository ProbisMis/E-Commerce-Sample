using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Trendyol.Scripts;

namespace TrendyolTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_AddProuctToCart()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Cart cart = new Cart();

            //Act
            cart.AddItem(Apple, 5);

            //Assert
            Assert.AreEqual(5, cart.TotalQuantity);

        }

        [TestMethod]
        public void Test_AddZeroProuctToCart()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Cart cart = new Cart();

            //Act
            cart.AddItem(Apple, 0);

            //Assert
            Assert.AreEqual(0, cart.TotalQuantity);
            Assert.AreEqual(0, cart.GetNumberOfProducts());
        }

        [TestMethod]
        public void Test_AddNegativeQuantityProuctToCart()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Cart cart = new Cart();

            //Act
            cart.AddItem(Apple, -10);

            //Assert
            Assert.AreEqual(0, cart.TotalQuantity);
            Assert.AreEqual(0, cart.GetNumberOfProducts());
        }

        [TestMethod]
        public void Test_GetProductsByCategory()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            List<Product> expectedProducts = new List<Product>();
            expectedProducts.Add(Apple);

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            List<Product> outcome = cart.GetProductsByCategory(Fruit);

            //Assert

            CollectionAssert.AreEqual(expectedProducts, outcome);

        }

        [TestMethod]
        public void Test_GetProductsByCategory_NullRefferenceException()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Automobile = new Category("Automobile");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Cart cart = new Cart();

            List<Product> expectedProducts = new List<Product>();

            //Act
            cart.AddItem(Apple, 5);
            List<Product> outcome = cart.GetProductsByCategory(Automobile);

            //Assert

            CollectionAssert.AreEqual(expectedProducts, outcome);

        }

        [TestMethod]
        public void Test_ApplyCampaignDiscount_On_DiscountType_Amount()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);
            Campaign campaign2 = new Campaign(Fruit, 10, 10, DiscountType.Rate);
            Campaign campaign3 = new Campaign(Vegetable, 10, 5, DiscountType.Rate);

            double expectedDiscountAmount = 10;

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);

            //Assert

            Assert.AreEqual(expectedDiscountAmount, cart.CampaignDiscountAmount);

        }

        [TestMethod]
        public void Test_ApplyCampaignDiscount_On_DiscountType_Rate()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);
            Campaign campaign2 = new Campaign(Fruit, 20, 5, DiscountType.Rate);
            Campaign campaign3 = new Campaign(Vegetable, 10, 5, DiscountType.Rate);

            double expectedDiscountAmount = 100;

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);

            //Assert

            Assert.AreEqual(expectedDiscountAmount, cart.CampaignDiscountAmount);

        }

        [TestMethod]
        public void Test_ApplyCampaignDiscount_NoCampaignApplicable()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 10, DiscountType.Amount);
            Campaign campaign2 = new Campaign(Fruit, 20, 6, DiscountType.Rate);
            Campaign campaign3 = new Campaign(Vegetable, 10, 4, DiscountType.Rate);

            double expectedDiscountAmount = 0;

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);

            //Assert

            Assert.AreEqual(expectedDiscountAmount, cart.CampaignDiscountAmount);

        }

        [TestMethod]
        public void Test_ApplyCouponDiscount_NoCouponApplicable()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            Coupon coupon = new Coupon(500, 10, DiscountType.Amount);

            double expectedDiscountAmount = 0;

            //Act
            cart.AddItem(Apple, 3);
            cart.AddItem(Onion, 2);
            cart.ApplyDiscounts(coupon);

            //Assert

            Assert.AreEqual(expectedDiscountAmount, cart.CouponDiscountAmount);

        }

        [TestMethod]
        public void Test_CampaignDiscount_After_CouponDiscount()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Cart cart = new Cart();
            Campaign campaign2 = new Campaign(Fruit, 20, 5, DiscountType.Rate);
            Coupon coupon = new Coupon(500, 10, DiscountType.Amount);

            double expectedFinalAmount = 490;

            //Act
            cart.AddItem(Apple, 5);
            cart.ApplyDiscounts(coupon);
            cart.ApplyDiscounts(campaign2);


            //Assert

            Assert.AreEqual(expectedFinalAmount, cart.FinalAmount);
        }
        

        [TestMethod]
        public void Test_ApplyCouponDiscount_On_DiscountType_Amount()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);
            Campaign campaign2 = new Campaign(Fruit, 20, 5, DiscountType.Rate);
            Campaign campaign3 = new Campaign(Vegetable, 10, 5, DiscountType.Rate);

            Coupon coupon = new Coupon(500, 10, DiscountType.Amount);

            double expectedFinalAmount = 540;

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);
            cart.ApplyDiscounts(coupon);

            //Assert

            Assert.AreEqual(expectedFinalAmount, cart.FinalAmount);

        }

        [TestMethod]
        public void Test_ApplyCouponDiscount_On_DiscountType_Rate()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);
            Campaign campaign2 = new Campaign(Fruit, 20, 5, DiscountType.Rate);
            Campaign campaign3 = new Campaign(Vegetable, 10, 5, DiscountType.Rate);

            Coupon coupon = new Coupon(500, 10, DiscountType.Rate);

            double expectedFinalAmount = 495;

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);
            cart.ApplyDiscounts(coupon);

            //Assert

            Assert.AreEqual(expectedFinalAmount, cart.FinalAmount);

        }

        [TestMethod]
        public void Test_ApplyDiscount_On_ParentCategory()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category OrangeFruits = new Category("OrangeFruits", Fruit);
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Orange = new Product("Orange", 50.0, OrangeFruits);
            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);

            double expectedFinalAmount = 390;

            //Act
            cart.AddItem(Apple, 3);
            cart.AddItem(Orange, 2);
            cart.ApplyDiscounts(campaign1);


            //Assert

            Assert.AreEqual(expectedFinalAmount, cart.FinalAmount);

        }
        [TestMethod]
        public void Test_ApplyDiscount_On_NullParentCategory()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Product Apple = new Product("Apple", 100.0, Fruit);

            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);

            double expectedFinalAmount = 490;

            //Act
            cart.AddItem(Apple, 5);
            cart.ApplyDiscounts(campaign1);


            //Assert
            Assert.AreEqual(expectedFinalAmount, cart.FinalAmount);

        }

        [TestMethod]
        public void Test_ApplyDiscount_On_FreeProduct()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Product Apple = new Product("Apple", 0, Fruit);

            Cart cart = new Cart();

            Campaign campaign1 = new Campaign(Fruit, 10, 5, DiscountType.Amount);

            double expectedFinalAmount = 0;

            //Act
            cart.AddItem(Apple, 5);
            cart.ApplyDiscounts(campaign1);


            //Assert

            Assert.AreEqual(expectedFinalAmount, cart.FinalAmount);

        }

        [TestMethod]
        public void Test_DeliveryCost()
        {
            //Arrange
            Category Fruit = new Category("Fruit");
            Category Vegetable = new Category("Vegetable");
            Product Apple = new Product("Apple", 100.0, Fruit);
            Product Onion = new Product("Onion", 50.0, Vegetable);
            Cart cart = new Cart();
            DeliveryCostCalculator dev = new DeliveryCostCalculator(2, 2, 3);
            double expectedDeliveryCost = 11;

            //Act
            cart.AddItem(Apple, 5);
            cart.AddItem(Onion, 3);
            double actualDeliveryCost = dev.calculateFor(cart);

            //Assert
            Assert.AreEqual(expectedDeliveryCost, actualDeliveryCost);
        }
    }
}

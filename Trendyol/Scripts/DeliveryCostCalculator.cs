namespace Trendyol.Scripts
{
    public class DeliveryCostCalculator
    {
        public double CostPerDelivery { get; set; }
        public double CostPerProduct { get; set; }
        public double FixedCost { get; set; }

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost = 2.99)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }
        
        public double calculateFor(Cart cart)
        {
            double deliveryCost = (CostPerDelivery * cart.GetNumberOfDistinctCategories()) + (CostPerProduct * cart.GetNumberOfProducts()) + FixedCost;
            cart.DeliveryAmount = deliveryCost; //TODO: SRP
            return deliveryCost;
        }
    }
}

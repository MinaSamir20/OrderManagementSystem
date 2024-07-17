namespace OrderManagementSystem.Application.Services.Discounts
{
    public class DiscountService
    {
        public decimal CalculateDiscount(decimal orderTotal)
        {
            if (orderTotal > DiscountTiers.Tier2Threshold)
            {
                return DiscountTiers.Tier2Discount;
            }
            else if (orderTotal > DiscountTiers.Tier1Threshold)
            {
                return DiscountTiers.Tier1Discount;
            }

            return 0m;
        }

        public decimal ApplyDiscount(decimal orderTotal, decimal discountRate)
        {
            return orderTotal - (orderTotal * discountRate);
        }
    }
}

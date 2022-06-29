namespace Checkout.Api.BussinessLogic.Helpers
{
    public static class TotalGrossHelper
    {
        public static decimal CalculateTotalGross(decimal totalNet, int vatPercentage)
        {
            var vat = totalNet * vatPercentage / 100;
            return totalNet + vat;
        }
    }
}

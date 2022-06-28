namespace Checkout.Api.BussinessLogic.Helpers
{
    public static class TotalGrossHelper
    {
        private static readonly int _vat = 10;
        public static decimal CalculateTotalGross(decimal totalNet)
        {

            return totalNet * _vat / 100;
        }
    }
}

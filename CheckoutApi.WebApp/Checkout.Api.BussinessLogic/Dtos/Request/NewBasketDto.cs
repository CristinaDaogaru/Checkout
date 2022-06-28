namespace Checkout.Api.BussinessLogic.Dtos.Request
{
    public class NewBasketDto
    {
        public string CustomerFullName { get; set; }
        public bool PaysVAT { get; set; }
    }
}

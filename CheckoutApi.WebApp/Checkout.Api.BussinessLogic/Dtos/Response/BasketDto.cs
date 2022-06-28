namespace Checkout.Api.BussinessLogic.Dtos.Response
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }

        public decimal TotalNet { get; set; }
        public decimal TotalGross { get; set; }

    }
}

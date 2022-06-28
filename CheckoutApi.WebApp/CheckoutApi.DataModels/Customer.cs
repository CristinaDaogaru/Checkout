using CheckoutApi.DataModels.Base;

namespace CheckoutApi.DataModels
{
    public class Customer : IEntity<int>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool PaysVAT { get; set; }
    }
}

using CheckoutApi.DataModels.Base;

namespace CheckoutApi.DataModels
{
    public class Customer : IEntity<int>
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public bool PaysVAT { get; set; }
    }
}

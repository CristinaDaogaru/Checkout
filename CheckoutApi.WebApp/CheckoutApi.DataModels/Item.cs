using CheckoutApi.DataModels.Base;

namespace CheckoutApi.DataModels
{
    public class Item : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ItemsInBasket> ItemsInBaskets { get; set; }
    }
}

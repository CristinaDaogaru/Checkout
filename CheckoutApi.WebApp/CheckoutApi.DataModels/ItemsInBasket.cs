using CheckoutApi.DataModels.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApi.DataModels
{
    public class ItemsInBasket : IEntity<int>
    {
        public int ID { get; set; }
        public int BasketId { get; set; }

        [ForeignKey("BasketId")]
        public Basket Basket { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}

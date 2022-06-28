using CheckoutApi.DataModels.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApi.DataModels
{
    public class Basket : IEntity<int>
    {
        public int ID { get; set; }
        public decimal TotalSpentAmount { get; set; }

        public bool IsPaid { get; set; }

        public bool Close { get; set; }

        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<ItemsInBasket> ItemsInBaskets { get; set; }
    }
}

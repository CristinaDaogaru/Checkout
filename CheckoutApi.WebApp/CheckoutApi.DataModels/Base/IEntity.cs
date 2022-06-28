namespace CheckoutApi.DataModels.Base
{
    public interface IEntity<TId>
    {
        TId ID { get; set; }
    }

    public interface IEntity : IEntity<object>
    {

    }
}

namespace CheckoutApi.DataModels.Base
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }

    public interface IEntity : IEntity<object>
    {

    }
}

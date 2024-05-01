
namespace SignalManagerAppWebApi.Models
{
    public interface IOrder
    {
        DateTime DateOrdered { get; set; }
        string OrderId { get; set; }
    }
}
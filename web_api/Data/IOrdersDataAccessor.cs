using SignalManagerAppWebApi.Models;

namespace SignalManagerAppWebApi.Data
{
    public interface IOrdersDataAccessor
    {
        void AddOrder(Order newOrder);
        void DeleteOrder(string orderId);
        List<Order> ReadOrders();
    }
}

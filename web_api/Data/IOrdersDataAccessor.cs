using SignalManagerAppWebApi.Models;

namespace SignalManagerAppWebApi.Data
{
    public interface IOrdersDataAccessor<T> where T:IOrder
    {
        void AddOrder(T newOrder);
        void DeleteOrder(string orderId);
        List<T> ReadOrders();
    }
}

using Newtonsoft.Json;
using SignalManagerAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SignalManagerAppWebApi.Data
{
    public class OrdersJsonDataAccessor : IOrdersDataAccessor
    {
        private readonly string _filePath;

        public OrdersJsonDataAccessor(string filePath)
        {
            _filePath = filePath;
        }

        public List<Order> ReadOrders()
        {
            string json = File.ReadAllText(_filePath);
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(json);
            return orders;
        }

        public void AddOrder(Order newOrder)
        {
            List<Order> orders = ReadOrders();
            orders.Add(newOrder);
            string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void DeleteOrder(string orderId)
        {
            List<Order> orders = ReadOrders();
            Order orderToRemove = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToRemove != null)
            {
                orders.Remove(orderToRemove);
                string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            else
            {
                throw new ArgumentException($"Order with ID '{orderId}' not found.");
            }
        }
    }
}

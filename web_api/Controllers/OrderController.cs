using Microsoft.AspNetCore.Mvc;
using SignalManagerAppWebApi.Data;
using SignalManagerAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalManagerAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersDataAccessor _ordersDataAccessor;

        public OrderController(IOrdersDataAccessor ordersDataAccessor)
        {
            _ordersDataAccessor = ordersDataAccessor;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _ordersDataAccessor.ReadOrders();
        }

        // GET api/<OrderController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Order order = _ordersDataAccessor.ReadOrders().FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] Order newOrder)
        {
            _ordersDataAccessor.AddOrder(newOrder);
            return CreatedAtAction(nameof(Get), new { id = newOrder.OrderId }, newOrder);
        }

        // DELETE api/<OrderController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _ordersDataAccessor.DeleteOrder(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}

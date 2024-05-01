﻿using Microsoft.AspNetCore.Mvc;
using SignalManagerAppWebApi.Data;
using SignalManagerAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalManagerAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellOrderController : ControllerBase
    {
        private readonly IOrdersDataAccessor<SellOrder> _ordersDataAccessor;

        public SellOrderController()
        {
            _ordersDataAccessor = new OrdersJsonDataAccessor<SellOrder>("test_data/test_sellorders.json");
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get()
        {
            var orders = _ordersDataAccessor.ReadOrders();
            return Ok(orders);
        }

        // GET api/<OrderController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var order = _ordersDataAccessor.ReadOrders().FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] SellOrder newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Order object is null");
            }

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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using SignalManagerAppWebApi.Models;
using SignalManagerAppWebApi.Data;

namespace SignalManagerAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        private readonly ISignalsDataAccessor _signalsDataAccessor;

        public SignalController(ISignalsDataAccessor signalsDataAccessor)
        {
            _signalsDataAccessor = signalsDataAccessor;
        }

        // GET api/<SignalController>/
        [HttpGet]
        public IActionResult Get()
        {
            var signals = _signalsDataAccessor.ReadSignals();
            return Ok(signals);
        }

        // GET api/<SignalController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var signals = _signalsDataAccessor.ReadSignals();
            var signal = signals.FirstOrDefault(s => s.Id == id);

            if (signal == null)
            {
                return NotFound();
            }

            return Ok(signal);
        }

        // POST api/<SignalController>
        [HttpPost]
        public IActionResult Post([FromBody] Signal newSignal)
        {
            if (newSignal == null)
            {
                return BadRequest("Signal object is null");
            }

            _signalsDataAccessor.AddSignal(newSignal);
            return CreatedAtAction(nameof(Get), new { id = newSignal.Id }, newSignal);
        }

        // DELETE api/<SignalController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _signalsDataAccessor.DeleteSignal(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using SignalManagerAppWebApi.Models;
using SignalManagerAppWebApi.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalManagerAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        private readonly ISignalsDataAccessor _signalsDataAccessor;
        public SignalController(ISignalsDataAccessor signlsDataAccessor)
        {
            _signalsDataAccessor = signlsDataAccessor;
        }

        // GET api/<SignalController>/
        [HttpGet]
        public IEnumerable<Signal> Get()
        {
            return _signalsDataAccessor.ReadSignals();
        }

        // GET api/<SignalController>/{id}
        [HttpGet("{id}")]
        public Signal Get(string id)
        {
            List<Signal> signals = _signalsDataAccessor.ReadSignals();
            Signal signal = signals.FirstOrDefault(s => s.Id == id);
            return signal;

        }

        // POST api/<SignalController>
        [HttpPost]
        public IActionResult Post([FromBody] Signal newSignal)
        {
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

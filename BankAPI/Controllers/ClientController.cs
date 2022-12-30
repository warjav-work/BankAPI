using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;
        public ClientController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _service.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetById(int id)
        {
            var client = _service.GetById(id);
            if (client is null)
            {
                return NotFound();
            }
            return client;
        }
        [HttpPost]
        public IActionResult Post(Client client)
        {
            var newClient = _service.Create(client);

            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            var clientToUpdate = _service.GetById(id);
            if (clientToUpdate is not null)
            {
                _service.Update(client);
                return NoContent();
            }
            else
            {
                return NotFound();
            }           

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var clientToDelete = _service.GetById(id);
            if (clientToDelete is not null)
            {
                _service.Delete(id);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

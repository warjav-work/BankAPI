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
        public async Task<IEnumerable<Client>> Get()
        {
            return await _service.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _service.GetById(id);
            if (client is null)
            {
                return NotFound();
            }
            return client;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            var newClient = await _service.Create(client);

            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            var clientToUpdate = await _service.GetById(id);
            if (clientToUpdate is not null)
            {
                await _service.Update(client);
                return NoContent();
            }
            else
            {
                return NotFound();
            }           

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clientToDelete = await _service.GetById(id);
            if (clientToDelete is not null)
            {
                await _service.Delete(id);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

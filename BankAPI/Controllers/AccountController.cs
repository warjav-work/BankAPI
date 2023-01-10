using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using BankAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly AccountTypeService _accountTypeService;
        private readonly ClientService _clientService;

        public AccountController(AccountService accountService, AccountTypeService accountTypeService, ClientService clientService)
        {
            _accountService = accountService;
            _accountTypeService = accountTypeService;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountDtoOut>> Get()
        {
            return await _accountService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDtoOut>> GetById(int id)
        {
            var account = await _accountService.GetDtoById(id);
            if (account is null)
            {
                return NotFound();
            }
            return account;


        }

        [Authorize(Policy ="SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(AccountDtoIn account)
        {
            string validationResult = await ValidateAccount(account);
            if (!validationResult.Equals("Valid"))
            {
                return BadRequest(new { message = validationResult });
            }
            var newAccount = await _accountService.Create(account);

            return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AccountDtoIn accountDTO)
        {
            if (id != accountDTO.Id)
            {
                return BadRequest(new { message = $"El ID({id}) de la URl no coincide con el ID({accountDTO.Id}) del cuerpo de la petición." });
            }

            var accountToUpdate = await _accountService.GetById(id);
            if (accountToUpdate is not null)
            {
                string validationResult = await ValidateAccount(accountDTO);

                if (!validationResult.Equals("Valid"))
                {
                    return BadRequest(new { message = validationResult });
                }
                await _accountService.Update(accountDTO);

                return NoContent();
            }
            else
            {
                return AccountNotFound(id);
            }

        }

        [Authorize(Policy = "SuperAdmin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var accountToDelete = await _accountService.GetById(id);
            if (accountToDelete is not null)
            {
                await _accountService.Delete(id);
                return Ok();
            }
            else
            {
                return AccountNotFound(id);
            }

        }

        public NotFoundObjectResult AccountNotFound(int id)
        {
            return NotFound(new { message = $"La cuenta con ID={id} no existe." });
        }

        private async Task<string> ValidateAccount(AccountDtoIn account)
        {
            string result = "Valid";
            var accountType = await _accountTypeService.GetById(account.AccountType);

            if (accountType is null)
            {
                result = $"El tipo de cuenta {account.AccountType} no existe.";
            }

            var clientID = account.ClientId.GetValueOrDefault();

            var client = await _clientService.GetById(clientID);

            if(client is null)
            {
                result = $"El cliente con ID({clientID}) no existe.";
            }

            return result;

        }
    }
}

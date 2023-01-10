using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Service
{
    public class AccountService
    {
        private readonly BankContext _context;

        public AccountService(BankContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountDtoOut>> GetAll()
        {
            return await _context.Accounts.Select(account => new AccountDtoOut
            {
                Id = account.Id,
                AccountName = account.AccountTypeNavigation.Name,
                ClientName = account.Client.Name != null ? account.Client.Name : "",
                Balance = account.Balance,
                RegDate = account.RegDate
            }).ToListAsync();

        }

        public async Task<AccountDtoOut?> GetDtoById(int id)
        {
            return await _context.Accounts.Where(account => account.Id == id).Select(account => new AccountDtoOut
            {
                Id = account.Id,
                AccountName = account.AccountTypeNavigation.Name,
                ClientName = account.Client.Name != null ? account.Client.Name : "",
                Balance = account.Balance,
                RegDate = account.RegDate
            }).SingleOrDefaultAsync();

        }
        public async Task<Account?> GetById(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> Create(AccountDtoIn newAccountDTO)
        {

            var newAccount = new Account()
            {
                AccountType = newAccountDTO.AccountType,
                ClientId = newAccountDTO.ClientId,
                Balance = newAccountDTO.Balance
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return newAccount;
        }
        public async Task Update(AccountDtoIn accountDTO)
        {
            var existingAccount = await GetById(accountDTO.Id);
            if (existingAccount is not null)
            {
                existingAccount.AccountType = accountDTO.AccountType;
                existingAccount.ClientId = accountDTO.ClientId;
                existingAccount.Balance = accountDTO.Balance;
                await _context.SaveChangesAsync();

            }
        }


        public async Task Delete(int id)
        {
            var existing = await GetById(id);
            if (existing is not null)
            {
                _context.Accounts.Remove(existing);
                await _context.SaveChangesAsync();

            }

        }
    }
}

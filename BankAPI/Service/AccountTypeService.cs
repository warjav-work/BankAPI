using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Service
{
    public class AccountTypeService
    {
        private readonly BankContext _context;

        public AccountTypeService(BankContext context)
        {
            _context = context;
        }

        public async Task<AccountType?> GetById(int id)
        {
            return await _context.AccountTypes.FindAsync(id);
        }
    }
}

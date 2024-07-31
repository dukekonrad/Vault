using Microsoft.EntityFrameworkCore;
using VaultContracts.StoragesContracts;
using VaultContracts.BindingModels;
using VaultContracts.ViewModels;
using VaultContracts.SearchModels;
using VaultDatabase.Models;

namespace VaultDatabase.Implements
{
    public class AccountStorage : IAccountStorage
    {
        private readonly VaultContext _context;

        public AccountStorage(VaultContext context)
        {
            _context = context;
        }

        public async Task<List<AccountViewModel>> GetFullList()
        {
            return await _context.Accounts
				    .OrderBy(x => x.Id)
					.Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Transactions.Sum(y => y.Amount)
                    }.GetViewModel)
                    .ToListAsync();
        }

        public async Task<List<AccountViewModel>> GetFilteredList(AccountSearchModel model)
        {
            if (!model.Id.HasValue && string.IsNullOrEmpty(model.Owner))
            {
                return new();
            }
            return await _context.Accounts
                    .Where(x => (model.Id.HasValue && x.Id == model.Id) || 
                        (!string.IsNullOrEmpty(model.Owner) && x.Owner.Contains(model.Owner)))
					.OrderBy(x => x.Id)
					.Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Transactions.Sum(y => y.Amount)
                    }.GetViewModel)
					.ToListAsync();
        }

        public async Task<AccountViewModel?> GetElement(AccountSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            Account? account = await _context.Accounts
                    .Where(x => x.Id == model.Id)
                    .Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Transactions.Sum(y => y.Amount)
                    })
                    .FirstOrDefaultAsync();
            return account != null ? account.GetViewModel : new();
        }

        public async Task<AccountViewModel?> Insert(AccountBindingModel model)
        {
            var newAccount = Account.Create(model);
            if (newAccount == null)
            {
                return null;
            }

            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            return newAccount.GetViewModel;
        }

        public async Task<AccountViewModel?> Update(AccountBindingModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (account == null)
            {
                return null;
            }

            account.Update(model);
            await _context.SaveChangesAsync();
            return account.GetViewModel;
        }

        public async Task<AccountViewModel?> Delete(AccountBindingModel model)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (account == null)
            {
				return null;
			}
            
			_context.Accounts.Remove(account);
			await _context.SaveChangesAsync();
			return account.GetViewModel;
		}
    }
}

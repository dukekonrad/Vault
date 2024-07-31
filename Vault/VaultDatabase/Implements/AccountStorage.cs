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

        public List<AccountViewModel> GetFullList()
        {
            return _context.Accounts
				    .OrderBy(x => x.Id)
					.Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Transactions.Sum(y => y.Amount)
                    }.GetViewModel)
                    .ToList();
        }

        public List<AccountViewModel> GetFilteredList(AccountSearchModel model)
        {
            if (!model.Id.HasValue && string.IsNullOrEmpty(model.Owner))
            {
                return new();
            }
            return _context.Accounts
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
					.ToList();
        }

        public AccountViewModel? GetElement(AccountSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            return _context.Accounts
                    .Where(x => x.Id == model.Id)
                    .Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Transactions.Sum(y => y.Amount)
                    })
                    .FirstOrDefault()?
                    .GetViewModel;
        }

        public AccountViewModel? Insert(AccountBindingModel model)
        {
            var newAccount = Account.Create(model);
            if (newAccount == null)
            {
                return null;
            }

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            return newAccount.GetViewModel;
        }

        public AccountViewModel? Update(AccountBindingModel model)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == model.Id);
            if (account == null)
            {
                return null;
            }

            account.Update(model);
            _context.SaveChanges();
            return account.GetViewModel;
        }

        public AccountViewModel? Delete(AccountBindingModel model)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == model.Id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return account.GetViewModel;
            }
            return null;
        }
    }
}

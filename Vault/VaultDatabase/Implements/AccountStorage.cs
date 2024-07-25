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
        public List<AccountViewModel> GetFullList()
        {
            using var context = new VaultContext();
            return context.Accounts
                    .Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Operations.Sum(y => y.Amount)
                    }.GetViewModel)
                    .ToList();
        }

        public List<AccountViewModel> GetFilteredList(AccountSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Owner))
            {
                return new();
            }
            using var context = new VaultContext();
            return context.Accounts
                    .Where(x => x.Owner.Contains(model.Owner))
                    .Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Operations.Sum(y => y.Amount)
                    }.GetViewModel)
                    .ToList();
        }

        public AccountViewModel? GetElement(AccountSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            using var context = new VaultContext();
            return context.Accounts
                    .Where(x => x.Id == model.Id)
                    .Select(x => new Account
                    {
                        Id = x.Id,
                        Owner = x.Owner,
                        Purpose = x.Purpose,
                        Balance = x.Operations.Sum(y => y.Amount)
                    })
                    .FirstOrDefault()?
                    .GetViewModel;
        }

        public AccountViewModel? Insert(AccountBindingModel model)
        {
            using var context = new VaultContext();
            var newAccount = Account.Create(model);
            if (newAccount == null)
            {
                return null;
            }

            context.Accounts.Add(newAccount);
            context.SaveChanges();
            return newAccount.GetViewModel;
        }

        public AccountViewModel? Update(AccountBindingModel model)
        {
            using var context = new VaultContext();
            var account = context.Accounts.FirstOrDefault(x => x.Id == model.Id);
            if (account == null)
            {
                return null;
            }

            account.Update(model);
            context.SaveChanges();
            return account.GetViewModel;
        }

        public AccountViewModel? Delete(AccountBindingModel model)
        {
            using var context = new VaultContext();
            var account = context.Accounts.FirstOrDefault(x => x.Id == model.Id);
            if (account != null)
            {
                context.Accounts.Remove(account);
                context.SaveChanges();
                return account.GetViewModel;
            }
            return null;
        }
    }
}

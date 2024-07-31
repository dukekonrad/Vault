using Microsoft.EntityFrameworkCore;
using VaultContracts.StoragesContracts;
using VaultContracts.BindingModels;
using VaultContracts.ViewModels;
using VaultContracts.SearchModels;
using VaultDatabase.Models;

namespace VaultDatabase.Implements
{
    public class TransactionStorage : ITransactionStorage
    {
        private readonly VaultContext _context;

        public TransactionStorage(VaultContext context)
        {
            _context = context;
        }

        public List<TransactionViewModel> GetFullList()
        {
            return _context.Transactions
				    .OrderBy(x => x.Id)
					.Select(x => x.GetViewModel)
					.ToList();
        }

        public List<TransactionViewModel> GetFilteredList(TransactionSearchModel model)
        {
            if (!model.AccountId.HasValue)
            {
                return new();
            }
            return _context.Transactions
                    .Include(x => x.Account)
                    .Where(x => x.AccountId == model.AccountId)
					.OrderBy(x => x.Id)
					.Select(x => x.GetViewModel)
					.ToList();
        }

        public TransactionViewModel? GetElement(TransactionSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            return _context.Transactions
                    .Include(x => x.Account)
                    .FirstOrDefault(x => x.Id == model.Id)?
                    .GetViewModel;
        }

        public TransactionViewModel? Insert(TransactionBindingModel model)
        {
            var newOperation = Transaction.Create(model, _context);
            if (newOperation == null)
            {
                return null;
            }

            _context.Transactions.Add(newOperation);
            _context.SaveChanges();
            return newOperation.GetViewModel;
        }

        public TransactionViewModel? Update(TransactionBindingModel model)
        {
            var operation = _context.Transactions.FirstOrDefault(x => x.Id == model.Id);
            if (operation == null)
            {
                return null;
            }

            operation.Update(model);
            _context.SaveChanges();
            return operation.GetViewModel;
        }

        public TransactionViewModel? Delete(TransactionBindingModel model)
        {
            var operation = _context.Transactions.FirstOrDefault(x => x.Id == model.Id);
            if (operation != null)
            {
                _context.Transactions.Remove(operation);
                _context.SaveChanges();
                return operation.GetViewModel;
            }
            return null;
        }
    }
}

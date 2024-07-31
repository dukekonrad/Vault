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

        public async Task<List<TransactionViewModel>> GetFullList()
        {
            return await _context.Transactions
				    .OrderBy(x => x.Id)
					.Select(x => x.GetViewModel)
					.ToListAsync();
        }

        public async Task<List<TransactionViewModel>> GetFilteredList(TransactionSearchModel model)
        {
            if (!model.AccountId.HasValue)
            {
                return new();
            }
            return await _context.Transactions
                    .Include(x => x.Account)
                    .Where(x => x.AccountId == model.AccountId)
					.OrderBy(x => x.Id)
					.Select(x => x.GetViewModel)
					.ToListAsync();
        }

        public async Task<TransactionViewModel?> GetElement(TransactionSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            var transaction = await _context.Transactions
                    .Include(x => x.Account)
                    .FirstOrDefaultAsync(x => x.Id == model.Id);
            return transaction != null ? transaction.GetViewModel : new();
        }

        public async Task<TransactionViewModel?> Insert(TransactionBindingModel model)
        {
            var newTransaction = Transaction.Create(model, _context);
            if (newTransaction == null)
            {
                return null;
            }

            await _context.Transactions.AddAsync(newTransaction);
            await _context.SaveChangesAsync();
            return newTransaction.GetViewModel;
        }

        public async Task<TransactionViewModel?> Update(TransactionBindingModel model)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (transaction == null)
            {
                return null;
            }

            transaction.Update(model);
            await _context.SaveChangesAsync();
            return transaction.GetViewModel;
        }

        public async Task<TransactionViewModel?> Delete(TransactionBindingModel model)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (transaction == null)
            {
				return null;
			}
            
			_context.Transactions.Remove(transaction);
			await _context.SaveChangesAsync();
			return transaction.GetViewModel;
		}
    }
}

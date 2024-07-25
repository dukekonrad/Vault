using Microsoft.EntityFrameworkCore;
using VaultContracts.StoragesContracts;
using VaultContracts.BindingModels;
using VaultContracts.ViewModels;
using VaultContracts.SearchModels;
using VaultDatabase.Models;

namespace VaultDatabase.Implements
{
    public class OperationStorage : IOperationStorage
    {
        private readonly VaultContext _context;

        public OperationStorage(VaultContext context)
        {
            _context = context;
        }

        public List<OperationViewModel> GetFullList()
        {
            return _context.Operations
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public List<OperationViewModel> GetFilteredList(OperationSearchModel model)
        {
            if (!model.AccountId.HasValue)
            {
                return new();
            }
            return _context.Operations
                    .Include(x => x.Account)
                    .Where(x => x.AccountId == model.AccountId)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public OperationViewModel? GetElement(OperationSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            return _context.Operations
                    .Include(x => x.Account)
                    .FirstOrDefault(x => x.Id == model.Id)?
                    .GetViewModel;
        }

        public OperationViewModel? Insert(OperationBindingModel model)
        {
            var newOperation = Operation.Create(model, _context);
            if (newOperation == null)
            {
                return null;
            }

            _context.Operations.Add(newOperation);
            _context.SaveChanges();
            return newOperation.GetViewModel;
        }

        public OperationViewModel? Update(OperationBindingModel model)
        {
            var operation = _context.Operations.FirstOrDefault(x => x.Id == model.Id);
            if (operation == null)
            {
                return null;
            }

            operation.Update(model);
            _context.SaveChanges();
            return operation.GetViewModel;
        }

        public OperationViewModel? Delete(OperationBindingModel model)
        {
            var operation = _context.Operations.FirstOrDefault(x => x.Id == model.Id);
            if (operation != null)
            {
                _context.Operations.Remove(operation);
                _context.SaveChanges();
                return operation.GetViewModel;
            }
            return null;
        }
    }
}

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
        public List<OperationViewModel> GetFullList()
        {
            using var context = new VaultContext();
            return context.Operations
                    .Select(x => x.GetViewModel)
                    .ToList();
        }
        
        public List<OperationViewModel> GetFilteredList(OperationSearchModel model)
        {
            if (!model.AccountId.HasValue)
            {
                return new();
            }
            using var context = new VaultContext();
            return context.Operations
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
            using var context = new VaultContext();
            return context.Operations
                    .Include(x => x.Account)
                    .FirstOrDefault(x => x.Id == model.Id)?
                    .GetViewModel;
        }

        public OperationViewModel? Insert(OperationBindingModel model)
        {
            using var context = new VaultContext();
            var newOperation = Operation.Create(model, context);
            if (newOperation == null)
            {
                return null;
            }
            
            context.Operations.Add(newOperation);
            context.SaveChanges();
            return newOperation.GetViewModel;
        }

        public OperationViewModel? Update(OperationBindingModel model)
        {
            using var context = new VaultContext();
            var operation = context.Operations.FirstOrDefault(x => x.Id == model.Id);
            if (operation == null)
            {
                return null;
            }

            operation.Update(model);
            context.SaveChanges();
            return operation.GetViewModel;
        }

        public OperationViewModel? Delete(OperationBindingModel model)
        {
            using var context = new VaultContext();
            var operation = context.Operations.FirstOrDefault(x => x.Id == model.Id);
            if (operation != null)
            {
                context.Operations.Remove(operation);
                context.SaveChanges();
                return operation.GetViewModel;
            }
            return null;
        }
    }
}

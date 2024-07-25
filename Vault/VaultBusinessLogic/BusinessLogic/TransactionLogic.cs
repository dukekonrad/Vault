using Microsoft.Extensions.Logging;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.StoragesContracts;
using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultBusinessLogic.BusinessLogic
{
    public class TransactionLogic : ITransactionLogic
    {
        private readonly ILogger _logger;
        private readonly ITransactionStorage _transactionStorage;

        public TransactionLogic(ILogger<TransactionLogic> logger, ITransactionStorage transactionStorage)
        {
            _logger = logger;
            _transactionStorage = transactionStorage;
        }

        public List<TransactionViewModel>? ReadList(TransactionSearchModel? model)
        {
            _logger.LogInformation("ReadList || Id: {Id}; AccountId: {AccountId}", model?.Id, model?.AccountId);
            var list = model == null ? _transactionStorage.GetFullList() : _transactionStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList || Return null list");
                return null;
            }
            _logger.LogInformation("ReadList || Count: {Count}", list.Count);
            return list;
        }

        public TransactionViewModel? ReadElement(TransactionSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement || Id: {Id}; AccountId: {AccountId}", model.Id, model.AccountId);
            var transaction = _transactionStorage.GetElement(model);
            if (transaction == null)
            {
                _logger.LogWarning("ReadElement || Transaction not found");
                return null;
            }
            _logger.LogInformation("ReadElement || Id: {Id}", transaction.Id);
            return transaction;
        }

        public bool Create(TransactionBindingModel model)
        {
            CheckModel(model);
            if (_transactionStorage.Insert(model) == null)
            {
                _logger.LogWarning("Create || Operation failed");
                return false;
            }
            return true;
        }

        public bool Update(TransactionBindingModel model)
        {
            CheckModel(model);
            if (_transactionStorage.Update(model) == null)
            {
                _logger.LogWarning("Update || Operation failed");
                return false;
            }
            return true;
        }

        public bool Delete(TransactionBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete || Id: {Id}", model.Id);
            if (_transactionStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete || Operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(TransactionBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            if (string.IsNullOrEmpty(model.Receiver))
            {
                throw new ArgumentNullException("Transaction's receiver missing", nameof(model.Receiver));
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                throw new ArgumentNullException("Transaction's description missing", nameof(model.Description));
            }

            _logger.LogInformation("Transaction || Id: {Id}; AccountId: {AccountId}; Receiver: {Receiver}; Description: {Description}; Amount: {Amount}; ExecutionDate: {ExecutionDate}",
                model.Id, model.AccountId, model.Receiver, model.Description, model.Amount, model.ExecutionDate);
        }
    }
}

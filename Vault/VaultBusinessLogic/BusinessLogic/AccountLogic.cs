using Microsoft.Extensions.Logging;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.StoragesContracts;
using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultBusinessLogic.BusinessLogic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly ILogger _logger;
        private readonly IAccountStorage _accountStorage;

        public AccountLogic(ILogger<AccountLogic> logger, IAccountStorage accountStorage)
        {
            _logger = logger;
            _accountStorage = accountStorage;
        }

        public async Task<List<AccountViewModel>?> ReadList(AccountSearchModel? model)
        {
            _logger.LogInformation("ReadList || Id: {Id}; Owner: {Owner}", model?.Id, model?.Owner);
            var list = model == null ? await _accountStorage.GetFullList() : await _accountStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList || Return null list");
                return null;
            }
            _logger.LogInformation("ReadList || Count: {Count}", list.Count);
            return list;
        }

        public async Task<AccountViewModel?> ReadElement(AccountSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement || Id: {Id}; Owner: {Owner}", model.Id, model.Owner);
            var account = await _accountStorage.GetElement(model);
            if (account == null)
            {
                _logger.LogWarning("ReadElement || Account not found");
                return null;
            }
            _logger.LogInformation("ReadElement || Id: {Id}", account.Id);
            return account;
        }

        public async Task<bool> Create(AccountBindingModel model)
        {
            CheckModel(model);
            if (await _accountStorage.Insert(model) == null)
            {
                _logger.LogWarning("Create || Operation failed");
                return false;
            }
            return true;
        }

        public async Task<bool> Update(AccountBindingModel model)
        {
            CheckModel(model);
            if (await _accountStorage.Update(model) == null)
            {
                _logger.LogWarning("Update || Operation failed");
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(AccountBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete || Id: {Id}", model.Id);
            if (await _accountStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete || Operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(AccountBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            if (string.IsNullOrEmpty(model.Owner))
            {
                throw new ArgumentNullException("Account's owner missing", nameof(model.Owner));
            }
            if (string.IsNullOrEmpty(model.Purpose))
            {
                throw new ArgumentNullException("Account's purpose missing", nameof(model.Purpose));
            }

            _logger.LogInformation("Account || Id: {Id}; Owner: {Owner}; Purpose: {Purpose}; Balance: {Balance}",
                model.Id, model.Owner, model.Purpose, model.Balance);
        }
    }
}

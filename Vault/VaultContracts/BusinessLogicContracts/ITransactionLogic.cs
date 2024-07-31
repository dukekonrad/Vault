using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.BusinessLogicContracts
{
    public interface ITransactionLogic
    {
        Task<List<TransactionViewModel>?> ReadList(TransactionSearchModel? model);

        Task<TransactionViewModel?> ReadElement(TransactionSearchModel model);

        Task<bool> Create(TransactionBindingModel model);

        Task<bool> Update(TransactionBindingModel model);

        Task<bool> Delete(TransactionBindingModel model);
    }
}

using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.StoragesContracts
{
    public interface ITransactionStorage
    {
		Task<List<TransactionViewModel>> GetFullList();

		Task<List<TransactionViewModel>> GetFilteredList(TransactionSearchModel model);

		Task<TransactionViewModel?> GetElement(TransactionSearchModel model);

		Task<TransactionViewModel?> Insert(TransactionBindingModel model);

		Task<TransactionViewModel?> Update(TransactionBindingModel model);

		Task<TransactionViewModel?> Delete(TransactionBindingModel model);
    }
}

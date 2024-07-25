using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.StoragesContracts
{
    public interface ITransactionStorage
    {
        List<TransactionViewModel> GetFullList();

        List<TransactionViewModel> GetFilteredList(TransactionSearchModel model);

        TransactionViewModel? GetElement(TransactionSearchModel model);

        TransactionViewModel? Insert(TransactionBindingModel model);

        TransactionViewModel? Update(TransactionBindingModel model);

        TransactionViewModel? Delete(TransactionBindingModel model);
    }
}

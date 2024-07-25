using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.BusinessLogicContracts
{
    public interface ITransactionLogic
    {
        List<TransactionViewModel>? ReadList(TransactionSearchModel? model);

        TransactionViewModel? ReadElement(TransactionSearchModel model);

        bool Create(TransactionBindingModel model);

        bool Update(TransactionBindingModel model);

        bool Delete(TransactionBindingModel model);
    }
}

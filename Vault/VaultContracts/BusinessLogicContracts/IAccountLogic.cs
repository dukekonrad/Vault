using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.BusinessLogicContracts
{
    public interface IAccountLogic
    {
        Task<List<AccountViewModel>?> ReadList(AccountSearchModel? model);

        Task<AccountViewModel?> ReadElement(AccountSearchModel model);

        Task<bool> Create(AccountBindingModel model);

        Task<bool> Update(AccountBindingModel model);

        Task<bool> Delete(AccountBindingModel model);
    }
}

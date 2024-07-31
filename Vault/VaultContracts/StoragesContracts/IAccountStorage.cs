using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.StoragesContracts
{
    public interface IAccountStorage
    {
        Task<List<AccountViewModel>> GetFullList();

        Task<List<AccountViewModel>> GetFilteredList(AccountSearchModel model);

		Task<AccountViewModel?> GetElement(AccountSearchModel model);

		Task<AccountViewModel?> Insert(AccountBindingModel model);

		Task<AccountViewModel?> Update(AccountBindingModel model);

		Task<AccountViewModel?> Delete(AccountBindingModel model);
    }
}

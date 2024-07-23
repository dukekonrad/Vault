using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.StoragesContracts
{
    public interface IAccountStorage
    {
        List<AccountViewModel> GetFullList();

        List<AccountViewModel> GetFilteredList(AccountSearchModel model);

        AccountViewModel? GetElement(AccountSearchModel model);

        AccountViewModel? Insert(AccountBindingModel model);

        AccountViewModel? Update(AccountBindingModel model);

        AccountViewModel? Delete(AccountBindingModel model);
    }
}

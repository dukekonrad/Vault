using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.BusinessLogicContracts
{
    public interface IAccountLogic
    {
        List<AccountViewModel>? ReadList(AccountSearchModel? model);

        AccountViewModel? ReadElement(AccountSearchModel model);

        bool Create(AccountBindingModel model);

        bool Update(AccountBindingModel model);

        bool Delete(AccountBindingModel model);
    }
}

using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.StoragesContracts
{
    public interface IOperationStorage
    {
        List<OperationViewModel> GetFullList();

        List<OperationViewModel> GetFilteredList(OperationSearchModel model);

        OperationViewModel? GetElement(OperationSearchModel model);

        OperationViewModel? Insert(OperationBindingModel model);

        OperationViewModel? Update(OperationBindingModel model);

        OperationViewModel? Delete(OperationBindingModel model);
    }
}

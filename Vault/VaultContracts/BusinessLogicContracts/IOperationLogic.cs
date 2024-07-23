using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultContracts.ViewModels;

namespace VaultContracts.BusinessLogicContracts
{
    public interface IOperationLogic
    {
        List<OperationViewModel>? ReadList(OperationSearchModel? model);

        OperationViewModel? ReadElement(OperationSearchModel model);

        bool Create(OperationBindingModel model);

        bool Update(OperationBindingModel model);

        bool Delete(OperationBindingModel model);
    }
}

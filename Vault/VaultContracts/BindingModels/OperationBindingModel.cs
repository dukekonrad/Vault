using VaultDataModels.Models;

namespace VaultContracts.BindingModels
{
    public class OperationBindingModel : IOperation
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime ExecutionDate { get; set; } = DateTime.Now;
    }
}

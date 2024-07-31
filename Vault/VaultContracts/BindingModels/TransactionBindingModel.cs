using VaultDataModels.Models;

namespace VaultContracts.BindingModels
{
    public class TransactionBindingModel : ITransaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Receiver { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Amount { get; set; }

        public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
    }
}

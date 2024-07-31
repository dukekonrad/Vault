using System.ComponentModel;
using VaultDataModels.Models;

namespace VaultContracts.ViewModels
{
    public class TransactionViewModel : ITransaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [DisplayName("Принимающий")]
        public string Receiver { get; set; } = string.Empty;

        [DisplayName("Описание")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Сумма")]
        public double Amount { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
    }
}

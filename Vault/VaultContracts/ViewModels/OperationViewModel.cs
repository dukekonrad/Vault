using System.ComponentModel;
using VaultDataModels.Models;

namespace VaultContracts.ViewModels
{
    public class OperationViewModel : IOperation
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [DisplayName("Сумма")]
        public double Amount { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Дата выполнения")]
        public DateTime ExecutionDate { get; set; } = DateTime.Now;
    }
}

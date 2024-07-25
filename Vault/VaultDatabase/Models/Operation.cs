using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaultDataModels.Models;
using VaultContracts.BindingModels;
using VaultContracts.ViewModels;

namespace VaultDatabase.Models
{
    public class Operation : IOperation
    {
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = new();

        [Required]
        public string Receiver { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; } = DateTime.Now;
        public static Operation Create(OperationBindingModel model, VaultContext context)
        {
            return new Operation()
            {
                Id = model.Id,
                AccountId = model.AccountId,
                Account = context.Accounts.FirstOrDefault(x => x.Id == model.AccountId),
                Receiver = model.Receiver,
                Description = model.Description,
                Amount = model.Amount,
                ExecutionDate = model.ExecutionDate
            };
        }

        public void Update(OperationBindingModel model)
        {
            Description = model.Description;
        }

        public OperationViewModel GetViewModel => new()
        {
            Id = Id,
            AccountId = AccountId,
            Receiver = Receiver,
            Description = Description,
            Amount = Amount,
            ExecutionDate = ExecutionDate
        };
    }
}

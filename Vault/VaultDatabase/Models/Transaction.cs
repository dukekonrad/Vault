using System.ComponentModel.DataAnnotations;
using VaultDataModels.Models;
using VaultContracts.BindingModels;
using VaultContracts.ViewModels;

namespace VaultDatabase.Models
{
    public class Transaction : ITransaction
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
        public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
        public static Transaction Create(TransactionBindingModel model, VaultContext context)
        {
            return new Transaction()
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

        public void Update(TransactionBindingModel model)
        {
            Description = model.Description;
        }

        public TransactionViewModel GetViewModel => new()
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

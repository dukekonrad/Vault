using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaultDataModels.Models;
using VaultContracts.BindingModels;
using VaultContracts.ViewModels;

namespace VaultDatabase.Models
{
    public class Account : IAccount
    {
        public int Id { get; set; }

        [Required]
        public string Owner { get; set; } = string.Empty;

        [Required]
        public string Purpose { get; set; } = string.Empty;

        [NotMapped]
        public double Balance { get; set; }

        [ForeignKey("AccountId")]
        public virtual List<Transaction> Transactions { get; set; } = new();

        public static Account Create(AccountBindingModel model)
        {
            return new Account()
            {
                Id = model.Id,
                Owner = model.Owner,
                Purpose = model.Purpose,
                Balance = 0
            };
        }

        public void Update(AccountBindingModel model)
        {
            Owner = model.Owner;
            Purpose = model.Purpose;
        }

        public AccountViewModel GetViewModel => new()
        {
            Id = Id,
            Owner = Owner,
            Purpose = Purpose,
            Balance = Balance
        };
    }
}

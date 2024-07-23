using System.ComponentModel;
using VaultDataModels.Models;

namespace VaultContracts.ViewModels
{
    public class AccountViewModel : IAccount
    {
        [DisplayName("Номер")]
        public int Id { get; set; }

        [DisplayName("ОГРН")]
        public string OGRN { get; set; } = string.Empty;

        [DisplayName("Назначение")]
        public string Purpose { get; set; } = string.Empty;

        [DisplayName("Баланс")]
        public double Balance { get; set; }
    }
}

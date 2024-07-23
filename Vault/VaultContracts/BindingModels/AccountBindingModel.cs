using VaultDataModels.Models;

namespace VaultContracts.BindingModels
{
    public class AccountBindingModel : IAccount
    {
        public int Id { get; set; }

        public string OGRN { get; set; } = string.Empty;

        public string Purpose { get; set; } = string.Empty;

        public double Balance { get; set; }
    }
}

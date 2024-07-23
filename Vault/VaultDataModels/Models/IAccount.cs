namespace VaultDataModels.Models
{
    public interface IAccount : IId
    {
        string OGRN { get; }

        string Purpose { get; }

        double Balance { get; }
    }
}

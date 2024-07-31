namespace VaultDataModels.Models
{
    public interface IAccount : IId
    {
        string Owner { get; }

        string Purpose { get; }

		decimal Balance { get; }
    }
}

namespace VaultDataModels.Models
{
    public interface ITransaction : IId
    {
        int AccountId { get; }

        string Receiver { get; }

        string Description { get; }

        decimal Amount { get; }

        DateTime ExecutionDate { get; }
    }
}

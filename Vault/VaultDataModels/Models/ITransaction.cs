namespace VaultDataModels.Models
{
    public interface ITransaction : IId
    {
        int AccountId { get; }

        string Receiver { get; }

        string Description { get; }

        double Amount { get; }

        DateTime ExecutionDate { get; }
    }
}

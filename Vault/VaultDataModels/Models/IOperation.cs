namespace VaultDataModels.Models
{
    public interface IOperation : IId
    {
        int AccountId { get; }

        double Amount { get; }

        string Description { get; }

        DateTime ExecutionDate { get; }
    }
}

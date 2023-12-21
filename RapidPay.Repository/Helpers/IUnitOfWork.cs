namespace RapidPay.Repository.Helpers;

public interface IUnitOfWork
{
    ICreditCardRepository CreditCardRepository{ get; }
    ITransactionRepository TransactionRepository { get; }

    Task<int> Complete();

}

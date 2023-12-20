namespace RapidPay.Repository.Helpers;

public interface IUnitOfWork
{
    ICreditCardRepository CreditCardRepository{ get; }

    Task<int> Complete();

}

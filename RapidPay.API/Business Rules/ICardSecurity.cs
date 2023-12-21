namespace RapidPay.API.Business_Rules;

public interface ICardSecurity
{
    bool IsCardValid(CreditCard creditCard);
    bool IsFundsAvailable(string cardNumber, decimal amount);
}

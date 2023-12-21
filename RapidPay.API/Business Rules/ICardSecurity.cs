using RapidPay.Domain;
using RapidPay.Model;

namespace RapidPay.API.Business_Rules;

public interface ICardSecurity
{
    bool IsCardValid(CreditCard creditCard);
    bool IsFundsAvailable(string cardNumber, decimal amount);
    Task<Guid> Authenticate(AuthenticateModel authenticateModel);
}

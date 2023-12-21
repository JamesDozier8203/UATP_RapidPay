using RapidPay.Model;

namespace RapidPay.API.Business_Rules;

public class CardSecurity : ICardSecurity
{
    private readonly IUnitOfWork _unitOfWork;

    public CardSecurity(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool IsCardValid(CreditCard creditCard)
    {
        return _unitOfWork.GetValidCard(creditCard) == null? false : true;
    }

    public bool IsFundsAvailable(string cardNumber, decimal amount)
    {
        return _unitOfWork.CheckFundsAvailable(cardNumber, amount) == null ? false : true;
    }
}

using RapidPay.Domain;
using RapidPay.Model;
using RapidPay.Repository.Helpers;

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
        return _unitOfWork.CreditCardRepository.GetValidCard(creditCard) == null? false : true;
    }

    public bool IsFundsAvailable(string cardNumber, decimal amount)
    {
        return _unitOfWork.CreditCardRepository.CheckFundsAvailable(cardNumber, amount) == null ? false : true;
    }

    public async Task<Guid> Authenticate(AuthenticateModel authenticateModel)
    {
        return await _unitOfWork.CreditCardRepository.GetCardIdByCardNumberPinCode(authenticateModel.CardNumber, authenticateModel.PinCode);
    }
    
}

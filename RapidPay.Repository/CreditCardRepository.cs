using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RapidPay.Domain;
using RapidPay.Repository;
using RapidPay.Repository.Helpers;

namespace RapidPay.Repository;

public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRepository
{
    public CreditCardRepository(DataContext context) : base(context) { }

    public async Task<(string, decimal)> GetBalance(string creditCardNumber, string pinCode)
    {
        var cardInfo = await table.Where(c => c.CardNumber == creditCardNumber && c.PinCode == pinCode)
                                     .Select( x => new 
                                     { 
                                         x.CardNumber, 
                                         x.Balance 
                                     }).FirstOrDefaultAsync();

        return (cardInfo == null? "" : cardInfo.CardNumber,
                cardInfo == null ? 0 : cardInfo.Balance);
    }

    public async Task<bool> GetValidCard(CreditCard creditCard)
    {
        var cardId = await table.Where(c => c.CardNumber.Equals(creditCard.CardNumber)
                                           && c.CardNumber.Length == 15
                                           && c.CardHolderName.Equals(creditCard.CardHolderName)
                                           && c.CardType == creditCard.CardType
                                           && c.ExpiryMonth == creditCard.ExpiryMonth
                                           && c.ExpiryYear == creditCard.ExpiryYear
                                           && c.SecurityCode == creditCard.SecurityCode)
                                     .Select(x => new
                                     {
                                         x.Id
                                     }).FirstOrDefaultAsync();

        return cardId == null ? false : true;
    }

    public async Task<bool> CheckFundsAvailable(string cardNumber, decimal amount)
    {
        var cardId = await table.Where(c => c.CardNumber == cardNumber
                                           && c.Balance >= amount)
                                     .Select(x => new
                                     {
                                         x.Id
                                     }).FirstOrDefaultAsync();

        return cardId == null ? false : true;
    }
}

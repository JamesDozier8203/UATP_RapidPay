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
}

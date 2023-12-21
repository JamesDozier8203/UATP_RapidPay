namespace RapidPay.Repository.Helpers;

using System;
using RapidPay.Domain;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public ICreditCardRepository CreditCardRepository { get; }
    public ITransactionRepository TransactionRepository { get; }

    public UnitOfWork(DataContext dataContext,
        ICreditCardRepository creditCardRepository,
        ITransactionRepository transactionRepository)
    {
        this._context = dataContext;
        this.CreditCardRepository = creditCardRepository;
        this.TransactionRepository = transactionRepository;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
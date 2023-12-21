using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RapidPay.Domain;
using RapidPay.Repository;
using RapidPay.Repository.Helpers;

namespace RapidPay.Repository;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(DataContext context) : base(context) { }

}

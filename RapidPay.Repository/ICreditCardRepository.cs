﻿
using RapidPay.Domain;
using RapidPay.Repository.Helpers;

namespace RapidPay.Repository;

public interface ICreditCardRepository: IGenericRepository<CreditCard>
{
    Task<(string, decimal)> GetBalance(string creditCardNumber, string pinCode);
}


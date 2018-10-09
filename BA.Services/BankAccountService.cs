using BA.Models;
using BA.Models.Enums;
using BA.Repositories;
using BA.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BA.Services
{
    public class BankAccountService :IBankAccountService
    {
        IBankAccountRepository _bankAccountRepository;
        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }
        public  Task<List<BankAccountModel>> GetAccounts()
        {
            try
            {
                return  _bankAccountRepository.GetAll();
            }
            catch (Exception ex)
            {
                ex.Log();
                return null;
            }
        }
        public async Task<BankAccountModel> GetAccount(string accountNumber)
        {
            try
            {               
                return await _bankAccountRepository.GetAccount(accountNumber);               
            }
            catch (Exception ex)
            {
                ex.Log();
                return null;
            }
        }
        public async Task<BankAccountModel> Transaction(string accountNumber,double amount,string currency,TransactionType type)
        {
            try
            {                
                var account =  await GetAccount(accountNumber);
                if (account.AccountNumber == "Invalid")
                {
                    return account;
                }
                else
                {
                    string baseCurrency = account.Currency;
                    if (baseCurrency != currency.ToUpper())
                    {
                        amount = await ConvertAmount(amount, currency, baseCurrency);
                    }
                    return await _bankAccountRepository.Transaction(accountNumber, amount, baseCurrency, type);
                }
                
            }
            catch (Exception ex)
            {
                ex.Log();
                return null;
            }
        }

        public async Task<double> GetFunds(string bankAccount, string currency)
        {
            try
            {
                BankAccountModel account = await GetAccount(bankAccount);
                double amount = account.Amount;
                if (currency != account.Currency)
                {
                    amount = await ConvertAmount(account.Amount, account.Currency, currency);
                }
                return amount;
            }
            catch (Exception ex)
            {
                return  -1;
            }
        }

        // this could be a WCF service...
        public async Task<double> ConvertAmount(double amount, string fromCurrency, string toCurrency)
        {           
           return _bankAccountRepository.Convert(amount, fromCurrency, toCurrency);           ;
        }
    }
}

using BA.Models;
using BA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Services
{
    public interface IBankAccountService
    {
        Task<List<BankAccountModel>> GetAccounts();
        Task<BankAccountModel> GetAccount(string accountNumber);
        Task<BankAccountModel> Transaction(string accountNumber, double amount, string currency,TransactionType type);
        Task<double> GetFunds(string bankAccount, string currency);
    }
}

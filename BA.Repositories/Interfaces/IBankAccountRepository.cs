using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BA.Entities;
using BA.Models;
using BA.Models.Enums;

namespace BA.Repositories
{
    public interface IBankAccountRepository:IRepository<BankAccount,BankAccountModel>
    {
        Task<List<BankAccountModel>> GetAll();
        Task<BankAccountModel> GetAccount(string number);
        Task<BankAccountModel> Transaction(string accountNumber, double amount, string currency,TransactionType type);
        double Convert(double amount, string fromAccount, string toAccount);
    }
}

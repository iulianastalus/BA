using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BA.Entities;
using BA.Models;
namespace BA.Repositories
{
    public interface ITransactionRepository:IRepository<Transaction,TransactionModel>
    {
        void TransactionSave(int? fromAccount, int? toAccount, double amount, int currency);
    }
}

using BA.Entities;
using BA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Repositories
{
    public class TransactionRepository:Repository<Transaction,TransactionModel>,ITransactionRepository
    {
        public TransactionRepository(DbContext _dbContext) : base(_dbContext)
        {
        }
        public void TransactionSave(int? fromAccount, int? toAccount, double amount, int currency)
        {
            try
            {
                List<SqlParameter> params1 = new List<SqlParameter>()
                {
                    fromAccount ==null ? new SqlParameter("fromAccount",SqlDbType.VarChar) {Value =DBNull.Value }
                                        :new SqlParameter("fromAccount",SqlDbType.VarChar) {Value =fromAccount },
                    toAccount ==null ?   new SqlParameter("toAccount",SqlDbType.VarChar) {Value =DBNull.Value } 
                                        :new SqlParameter("toAccount",SqlDbType.VarChar) {Value =toAccount },
                    new SqlParameter("amount",SqlDbType.Float) {Value =amount },
                    new SqlParameter("currency",SqlDbType.Int) {Value =currency },
                    new SqlParameter("transactionDate",SqlDbType.DateTime) {Value =DateTime.Now }                    
                };
                _dbContext.Database.ExecuteSqlCommand("exec TransactionSave @fromAccount, @toAccount,@amount,@currency,@transactionDate"
                , params1.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

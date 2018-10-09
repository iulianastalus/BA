using BA.Entities;
using BA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Repositories
{
    public class CurrenciesRepository:Repository<Currency,CurrencyModel>,ICurrencyRepository
    {
        public CurrenciesRepository(DbContext _dbContext) : base(_dbContext)
        {            
        }
        public List<string> GetAllCurrencies()
        {
            return _dbSet.Select(x => x.Name).ToList();
        }
    }
}

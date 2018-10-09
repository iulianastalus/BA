using BA.Entities;
using BA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Repositories
{
    public interface ICurrencyRepository : IRepository<Currency,CurrencyModel>
    {
        List<string> GetAllCurrencies();
    }
}

using BA.Entities;
using BA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BA.Models.Enums;

namespace BA.Repositories
{
    public class BankAccountRepository:Repository<BankAccount,BankAccountModel>, IBankAccountRepository
    {
        MapperConfiguration _mappingConfig;
        ITransactionRepository _transactionRepository;
        IMapper _mapper;
        public BankAccountRepository(DbContext _dbContext, ITransactionRepository transactionRepository) : base(_dbContext)
        {
            _transactionRepository = transactionRepository;
            _mappingConfig = new MapperConfiguration(cfg => cfg.CreateMap<BankAccount, BankAccountModel>().ForMember(x=>x.AccountHolderName, 
                y=>y.MapFrom(z=>string.Concat(z.AccountHolder.FirstName," ",z.AccountHolder.LastName))).ForMember(x=>x.Currency,y=>y.MapFrom(z=>z.Currency.Name)));
            _mapper = _mappingConfig.CreateMapper();
        }
        public async Task<List<BankAccountModel>> GetAll()
        {
            IQueryable<BankAccount> query = _dbSet;
            return _mapper.Map<IQueryable<BankAccount>, List<BankAccountModel>>(query);
        }
        public async Task<BankAccountModel> GetAccount(string number)
        {
            BankAccount account = await _dbSet.FirstOrDefaultAsync(z => z.AccountNumber == number);
            if(account !=null)
                return  _mapper.Map<BankAccount, BankAccountModel>(account);
            return null;
        }

        public async Task<BankAccountModel> Transaction(string accountNumber, double amount, string currency,TransactionType type)
        {
            Currency currencyObj = _dbContext.Set<Currency>().FirstOrDefault(x => x.Name == currency);            
            BankAccount account = _dbSet.FirstOrDefault(z => z.AccountNumber == accountNumber);
            if (account == null)
            {
                return null;
            }
            else
            {
                switch (type)
                {
                    case TransactionType.DEPOSIT:
                        account.Amount += amount;
                        _transactionRepository.TransactionSave(null, account.BankAccountId, amount, currencyObj.CurrencyId);
                        break;
                    case TransactionType.WITHDRAW:
                        account.Amount -= amount;
                        _transactionRepository.TransactionSave(account.BankAccountId, null, -amount, currencyObj.CurrencyId);
                        break;
                }
                _dbContext.SaveChanges();
                return _mapper.Map<BankAccount, BankAccountModel>(account);
            }
            
        }       

        public double Convert(double amount, string fromAccount, string toAccount)
        {
            Rate rate = _dbContext.Set<Rate>().FirstOrDefault(z => (z.Currency.Name == fromAccount && z.Currency1.Name == toAccount) || (z.Currency.Name == toAccount && z.Currency1.Name == fromAccount));
            if (rate == null)
            {
                return 0;
            }
            else
            {
                if (rate.Currency.Name == fromAccount)
                {
                    return amount * rate.Rate1;
                }
                else
                {
                    return amount / rate.Rate1;
                }
            }
            
        }
    }
}

using BA.Repositories;
using BA.Services;
using Moq;
using NUnit.Framework;
using BA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using BA.Models.Enums;

namespace BA.Tests
{
    [TestFixture]
    public class BankAccountTest
    {
        Mock<IBankAccountRepository> _mockBankAccountRepository;
        IBankAccountService _bankAccountService;
        [SetUp]
        public void LoadContext()
        {
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _bankAccountService = new BankAccountService(_mockBankAccountRepository.Object);
        }
        [Test]
        public async Task get_account_should_be_null()
        {
            string accountNumber = "4fdd-dfff-wwee-2345";
            Task<BankAccountModel> result = Task.FromResult<BankAccountModel>(null);
            _mockBankAccountRepository.Setup(x => x.GetAccount(accountNumber)).Returns(result);
            var model =await _bankAccountService.GetAccount(accountNumber);
            model.Should().BeNull();
        }
        [Test]
        public async Task get_account_should_not_be_null()
        {
            string accountNumber = "1234-6789-2234-2234";
            Task<BankAccountModel> result = Task.FromResult<BankAccountModel>(new BankAccountModel
            {
                AccountNumber = "1234-6789-2234-2234",
                AccountHolderName = "Jim Joe",
                Amount =3255,
                Currency = "CAD"
            });
            _mockBankAccountRepository.Setup(x => x.GetAccount(accountNumber)).Returns(result);
            var model = await _bankAccountService.GetAccount(accountNumber);
            model.Should().NotBeNull();
        }
        [Test]
        public void get_balance_should_not_be_null()
        {
            string accountNumber = "1233-3333-1111-1123";
            BankAccountModel bankAccount = new BankAccountModel
            {
                AccountNumber = "1233-3333-1111-1123",
                AccountHolderName = "Astalus",
                Amount = 24,
                Currency = "USD"
            };
            _mockBankAccountRepository.Setup(x => x.GetAccount(accountNumber)).Returns(Task.FromResult(bankAccount));
            _bankAccountService.GetAccount(accountNumber).Should().NotBeNull();
        }
        [Test]
        public async Task transaction_should_be_invalid()
        {
            string accountNumber = "4fdd-dfff-wwee-2345";
            var result = await Task.FromResult<BankAccountModel>(new BankAccountModel {AccountNumber = "Invalid" });
            _mockBankAccountRepository.Setup(x => x.GetAccount(accountNumber)).Returns(Task.FromResult(result));
             var response =await _bankAccountService.Transaction(accountNumber, It.IsAny<double>(), It.IsAny<string>(), It.IsAny<TransactionType>());
            response.Should().Be(result);
        }

        [Test]
        public async Task transaction_should_not_be_null()
        {
            var result = await Task.FromResult<BankAccountModel>(new BankAccountModel
            {
                AccountNumber = "1233-3333-1111-1123",
                AccountHolderName = "Astalus",
                Amount = 5589,
                Currency = "USD"
            });
            _mockBankAccountRepository.Setup(x => x.GetAccount("1233-3333-1111-1123")).Returns(Task.FromResult<BankAccountModel>(result));
            _mockBankAccountRepository.Setup(x => x.Convert(300, "THB", "USD")).Returns(5554);
            result.Amount = 5589 + 5554;
            _mockBankAccountRepository.Setup(x => x.Transaction("1233-3333-1111-1123", 5554, "USD",TransactionType.DEPOSIT)).Returns(Task.FromResult<BankAccountModel>(result));
            var transactionResult = await _bankAccountService.Transaction("1233-3333-1111-1123", 300, "THB", TransactionType.DEPOSIT);
            transactionResult.Should().Be(result);
        }
    }
}

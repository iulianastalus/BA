using BA.Models;
using BA.Models.Enums;
using BA.Services;
using BAAPI.Controllers;
using BAAPI.ViewModels;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace BA.Tests
{
    [TestFixture]
    public class BankAccountAPITest
    {
        Mock<IBankAccountService> _mockBankAccountService;
        BankAccountsController _bancAccountController;
        [SetUp]
        public void LoadContext()
        {
            _mockBankAccountService = new Mock<IBankAccountService>();
            _bancAccountController = new BankAccountsController(_mockBankAccountService.Object);
            _bancAccountController.Request = new HttpRequestMessage();
             var config = new HttpConfiguration();
            _bancAccountController.Request.SetConfiguration(config);
        }
        [Test]
        public async Task get_balance_should_return_error()
        {
            try
            {
                string number = "423434324";
                var result = Task.FromResult<BankAccountModel>(null);                
                _mockBankAccountService.Setup(x => x.GetAccount(number)).Returns(result);
                var response = await _bancAccountController.Balance(number) as HttpResponseMessage;
                var content = response.Content as ObjectContent;
                ResponseMessage responseMessage = content.Value as ResponseMessage;
                responseMessage.Message.Should().Be("Invalid Acount Number");
                responseMessage.Successfull.Should().BeFalse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Test]
        public async Task wthdraw_usd_10000_should_not_work()
        {
            DepositWithDrawViewModel model = new DepositWithDrawViewModel
            {
                AccountNumber = "3355-3346-1466-7893",
                Amount = 10000,
                Currency = "USD"
            };
            _mockBankAccountService.Setup(x => x.GetFunds(model.AccountNumber, "USD")).Returns(Task.FromResult<double>(5434));
            var response = await _bancAccountController.Withdraw(model) as HttpResponseMessage;
            var content = response.Content as ObjectContent;
            ResponseMessage responseMessage = content.Value as ResponseMessage;
            responseMessage.Message.Should().Be("Not Enough funds");
            responseMessage.Successfull.Should().BeFalse();
        }

        [Test]
        public async Task wthdraw_usd_500_should_work()
        {
            DepositWithDrawViewModel model = new DepositWithDrawViewModel
            {
                AccountNumber = "3355-3346-1466-7893",
                Amount = 500,
                Currency = "USD"
            };
            _mockBankAccountService.Setup(x => x.GetFunds(model.AccountNumber, "USD")).Returns(Task.FromResult<double>(5434));
            var resultObj = Task.FromResult<BankAccountModel>(new BankAccountModel
            {
                AccountNumber = "3355-3346-1466-7893",
                AccountHolderName = "Jack Daniels",
                Amount = 4934,
                Currency = "USD"
            });
            _mockBankAccountService.Setup(x => x.Transaction(model.AccountNumber, model.Amount, model.Currency, TransactionType.WITHDRAW)).Returns(resultObj);
            var response = await _bancAccountController.Withdraw(model) as HttpResponseMessage;            
            var content = response.Content as ObjectContent;
            ((ResponseMessage)content.Value).Successfull.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);         
        }
    }
}

using BA.Models;
using BA.Models.Enums;
using BA.Services;
using BAAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BAAPI.Controllers
{    
    public class BankAccountsController : ApiController
    {
        IBankAccountService _bankAccountService;      
        public BankAccountsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }
        [Route("api/BankAccounts")]
        public async Task<HttpResponseMessage> GetAccounts()
        {
            var message = await _bankAccountService.GetAccounts();
            return await Task.FromResult(
                Request.CreateResponse(HttpStatusCode.OK,message)
             );
        }
        [HttpGet]
        [Route("api/BankAccounts/{number}/Balance")]
        public async Task<HttpResponseMessage> Balance(string number)
        {            
            var result = await _bankAccountService.GetAccount(number);
            if (result != null)
            {
                return await Task.FromResult(
                Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                {
                    Successfull = true,
                    Message = "Account loaded successfuly",
                    AccountNumber = result.AccountNumber,
                    Balance = result.Amount,
                    Currency = result.Currency
                }));
            }
            else
            {
                return await Task.FromResult(
                Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                {
                    Successfull = false,
                    Message ="Invalid Acount Number",
                    AccountNumber ="Invalid",
                    Balance =null,
                    Currency =null                  
                }));
            }            
        }
        [HttpPut]
        [Route("api/BankAccounts/Deposit")]
        public async Task<HttpResponseMessage> Deposit(DepositWithDrawViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _bankAccountService.Transaction(model.AccountNumber,model.Amount,model.Currency,TransactionType.DEPOSIT);
                if (result != null)
                {
                    if (result.AccountNumber == "Invalid")
                    {
                        return await Task.FromResult(
                        Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                        {
                            Successfull = false,
                            AccountNumber = "Invalid",
                            Balance = null,
                            Message = "Invalid Account Number",
                            Currency = null
                        }));
                    }
                    return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                    {
                        Successfull = true,
                        AccountNumber = result.AccountNumber,
                        Balance = result.Amount,
                        Currency =result.Currency
                    }));
                }
                else
                {
                    return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                    {
                        AccountNumber = null,
                        Balance = null,
                        Message = "Unexpected error",
                        Currency = null,
                        Successfull = false
                    }));
                }                              
            }
            return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.InternalServerError, ModelState.Values.SelectMany(z=>z.Errors)));
        }
        [HttpDelete]
        [Route("api/BankAccounts/Withdraw")]
        public async Task<HttpResponseMessage> Withdraw(DepositWithDrawViewModel model)
        {
            if (ModelState.IsValid)
            {
                double availableFunds = await _bankAccountService.GetFunds(model.AccountNumber, model.Currency);
                if (availableFunds < model.Amount)
                {
                    return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                    {
                        AccountNumber = model.AccountNumber,
                        Balance = null,
                        Message = "Not Enough funds",
                        Currency = model.Currency,
                        Successfull = false
                    }));
                }
                var result = await _bankAccountService.Transaction(model.AccountNumber, model.Amount, model.Currency,TransactionType.WITHDRAW);
                if (result != null)
                {
                    if (result.AccountNumber == "Invalid")
                    {
                        return await Task.FromResult(
                        Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                        {
                            Successfull = false,
                            AccountNumber = "Invalid",
                            Balance = null,
                            Message = "Invalid Account Number",
                            Currency = null
                        }));
                    }
                    return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                    {
                        AccountNumber = model.AccountNumber,
                        Balance = result.Amount,
                        Message = "Withdraw Succsessfull",
                        Currency = result.Currency,
                        Successfull = true
                    }));
                }
                else
                {
                    return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage
                    {
                        AccountNumber = model.AccountNumber,
                        Balance = null,
                        Message = "Unexpected error",
                        Currency = null,
                        Successfull = false
                    }));
                }
            }
            return await Task.FromResult(
                    Request.CreateResponse(HttpStatusCode.InternalServerError, ModelState.Values.SelectMany(z => z.Errors)));
        }
    }
}

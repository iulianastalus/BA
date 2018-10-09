using BA.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAAPI.ViewModels
{
    public class DepositWithDrawViewModel
    {
        [Required()]
        [IsAccountNumber(ErrorMesage ="Invalid account number!")]
        public string AccountNumber { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Amount should be positive")]
        public double Amount { get; set; }

        [IsCurrency()]
        public string Currency { get; set; }
    }
}
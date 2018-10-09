using BA.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Models
{
    public class BankAccountModel
    {
        public string AccountHolderName { get; set; }

        [IsAccountNumber(ErrorMessage ="This is not a card number")]
        [Required(ErrorMessage ="Card Number Is Required")]
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}

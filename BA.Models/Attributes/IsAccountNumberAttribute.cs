using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BA.Models.Attributes
{
    public class IsAccountNumberAttribute:ValidationAttribute
    {
        public const string DefaultErrorMessage = "Account is not valid";
        public string ErrorMesage { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            

            if (value !=null  && CheckValidity(value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMesage ?? DefaultErrorMessage);
        }

        bool CheckValidity(string accountNumber)
        {
            Regex reg = new Regex("[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}");
            Match m = reg.Match(accountNumber);
            return m.Success;
        }
    }
}

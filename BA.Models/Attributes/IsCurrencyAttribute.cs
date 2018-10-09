using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA.Models.Attributes
{

    public class IsCurrencyAttribute : ValidationAttribute
    {           
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            List<string> currencies = new List<string>{ "USD", "EUR", "CAD", "THB", "JPY","GBP" };

            if (currencies.Contains(value.ToString().ToUpper()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Not a currency");
        }
    }    
}

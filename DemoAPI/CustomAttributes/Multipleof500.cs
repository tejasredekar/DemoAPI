using System.ComponentModel.DataAnnotations;

namespace DemoAPI.CustomAttributes

{
    public class Multipleof500:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            double val = (double)value;

            int valueAsInt = (int)val;
            if (val - valueAsInt != 0)
            {
                return new ValidationResult("Value must be a multiple of 500 and must not contain any decimals");
            }
                if (valueAsInt % 500 == 0)
                {
                return ValidationResult.Success;
            }
            
            else
            {
                return new ValidationResult("Value must be a multiple of 500");
            }
        }
    }

}

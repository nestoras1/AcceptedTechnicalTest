using System.ComponentModel.DataAnnotations;

namespace AcceptedTechnicalTest.Common.Validators
{
    public class OddValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is double oddValue)
            {
                return oddValue > 1;
            }

            return false; // Invalid if the value is not a double
        }
    }
}

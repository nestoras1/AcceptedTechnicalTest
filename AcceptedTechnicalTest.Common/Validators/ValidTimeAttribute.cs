using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AcceptedTechnicalTest.Common.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidTimeAttribute : ValidationAttribute
    {
        private readonly string _pattern;

        public ValidTimeAttribute()
        {
            _pattern = @"^(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9])$";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string input = Convert.ToString(value);
            return Regex.IsMatch(input, _pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be in the format HH:mm";
        }
    }
}

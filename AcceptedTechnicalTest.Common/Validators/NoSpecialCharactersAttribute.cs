using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AcceptedTechnicalTest.Common.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoSpecialCharactersAttribute : ValidationAttribute
    {
        private readonly string _pattern;

        public NoSpecialCharactersAttribute()
        {
            _pattern = "[^a-zA-Z0-9]";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string input = Convert.ToString(value);
            return !Regex.IsMatch(input, _pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} can only contain letters, numbers, and spaces.";
        }
    }
}

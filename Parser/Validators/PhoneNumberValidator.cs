using PhoneBook.Models;

namespace PhoneBook.Parser.Validators
{
    class PhoneNumberValidator : IParsedRowValidator
    {
        public ValidationResult Validate(ParsedRow parsedRow)
        {
            var phoneNumber = parsedRow.PhoneNumber;

            if (phoneNumber.Length == 9 && phoneNumber.All(char.IsDigit))
            {
                return ValidationResult.Valid();
            }
            else
            {
                return ValidationResult.Invalid("Phone number should be with 9 Digits.");
            }
        }
    }
}

using PhoneBook.Models;

namespace PhoneBook.Parser.Validators
{
    class SeparatorValidator : IParsedRowValidator
    {
        public ValidationResult Validate(ParsedRow parsedRow)
        {
            var separator = parsedRow.Separator;

            if (separator == "-" || separator == ":")
            {
                return ValidationResult.Valid();
            }
            else
            {
                return ValidationResult.Invalid("The separator should be `:` or `-`.");
            }
        }
    }
}

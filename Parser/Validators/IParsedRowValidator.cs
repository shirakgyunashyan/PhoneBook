using PhoneBook.Models;

namespace PhoneBook.Parser.Validators
{ 
    internal interface IParsedRowValidator
    {
        ValidationResult Validate(ParsedRow parsedRow);
    }
}

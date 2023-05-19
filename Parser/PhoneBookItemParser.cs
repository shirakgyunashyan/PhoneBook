using PhoneBook.Domain;
using PhoneBook.Models;
using PhoneBook.Parser.Validators;

namespace PhoneBook.Parser
{
    internal class PhoneBookItemParser
    {
        public ParseResult Parse(string input)
        {
            var splited = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);

            var parsedRow = new ParsedRow();

            parsedRow.Name = splited[0];
            if (splited.Length == 4)
            {
                parsedRow.Surname = splited[1];
                parsedRow.Separator = splited[2];
                parsedRow.PhoneNumber = splited[3];
            }
            else if (splited.Length == 3)
            {
                parsedRow.Surname = "";
                parsedRow.Separator = splited[1];
                parsedRow.PhoneNumber = splited[2];
            }


            var validators = GetValidators();
            var validationIssues = new List<string>();

            foreach (var validator in validators)
            {
                var validationResult = validator.Validate(parsedRow);

                if (validationResult.IsValid)
                    continue;

                validationIssues.Add(validationResult.ErrorMessage!);
            }

            if (validationIssues.Any())
            {
                return new ParseResult
                {
                    ErrorMessages = validationIssues
                };
            }

            return new ParseResult
            {
                PhoneBookItem = new PhoneBookItem
                {
                    Name = parsedRow.Name,
                    Surname = parsedRow.Surname,
                    PhoneNumber = parsedRow.PhoneNumber
                }
            };
        }

        private IEnumerable<IParsedRowValidator> GetValidators()
        {
            yield return new PhoneNumberValidator();
            yield return new SeparatorValidator();
        }
    }
}

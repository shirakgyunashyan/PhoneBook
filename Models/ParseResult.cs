using PhoneBook.Domain;

namespace PhoneBook.Models
{
    internal class ParseResult
    {
        public bool IsSuccess => !ErrorMessages?.Any() ?? true;
        public PhoneBookItem? PhoneBookItem { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}

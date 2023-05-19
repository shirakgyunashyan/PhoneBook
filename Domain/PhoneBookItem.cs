using System.Reflection.Metadata;

namespace PhoneBook.Domain
{
    internal class PhoneBookItem
    {
        public string Name { get; init; } = null!;
        public string? Surname { get; init; }
        public string PhoneNumber { get; init; } = null!;

        private string _phoneNumberCode;
        public string PhoneNumberCode
        {
            get
            {
                if (string.IsNullOrEmpty(_phoneNumberCode))
                {
                    _phoneNumberCode = PhoneNumber.Substring(0, 3);
                }

                return _phoneNumberCode;
            }
        }


    }
}

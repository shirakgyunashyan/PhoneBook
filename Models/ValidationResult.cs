namespace PhoneBook.Models
{
    internal class ValidationResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }


       
        
        public static ValidationResult Valid()
        {
            return new ValidationResult { IsValid = true };
        }

        public static ValidationResult Invalid(string errorMessage)
        {
            return new ValidationResult { IsValid = false, ErrorMessage = errorMessage };
        }
    }
}

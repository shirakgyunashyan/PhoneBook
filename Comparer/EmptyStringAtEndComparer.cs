namespace PhoneBook.Comparer
{
    public class EmptyStringAtEndComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y))
            {
                return 0; // Both are empty strings, consider them equal
            }
            else if (string.IsNullOrEmpty(x))
            {
                return 1; // x is an empty string, place it at the end
            }
            else if (string.IsNullOrEmpty(y))
            {
                return -1; // y is an empty string, place it at the end
            }
            else
            {
                return x.CompareTo(y); // Compare non-empty strings as usual
            }
        }
    }
}

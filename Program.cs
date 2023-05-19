using PhoneBook;
using PhoneBook.Domain;
using PhoneBook.Parser;

internal class Program
{
    private static void Main(string[] args)
    {
        var lines = ReadFile();
        var parser = new PhoneBookItemParser();

        var phoneBookItems = new List<PhoneBookItem>();
        var failedLines = new List<(int lineNumber, IEnumerable<string> errors)>();

        Console.WriteLine("File Structure:");
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            Console.WriteLine(line);

            var parseResult = parser.Parse(line);
            if (!parseResult.IsSuccess)
            {
                failedLines.Add((lineNumber: i + 1, errors: parseResult.ErrorMessages!));
            }
            else
            {
                phoneBookItems.Add(parseResult.PhoneBookItem!);
            }
        }

        Console.WriteLine(Environment.NewLine);

        if (phoneBookItems.Any())
        {
            var orderedItems = Order(phoneBookItems);
            PrintOrderedItems(orderedItems);
        }

        Console.WriteLine(Environment.NewLine);

        if (failedLines.Any())
        {
            Console.WriteLine("Validations:");
            foreach (var (lineNumber, errors) in failedLines)
            {
                var fullErrorMessage = string.Join(" ", errors);
                Console.Write($"line{lineNumber}:{fullErrorMessage}");
                Console.WriteLine();
            }
        }

        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Done");
    }

    private static string[] ReadFile()
    {
        Console.Write("File Path - ");

        var filePath = Console.ReadLine();
        Console.WriteLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist");
            throw new InvalidOperationException();
        }

        var lines = File.ReadAllLines(filePath);
        return lines;
    }
    private static void PrintOrderedItems(List<PhoneBookItem> sortedItems)
    {
        Console.WriteLine("Sorted Items:");
        foreach (var item in sortedItems)
        {
            Console.WriteLine($"{item.Name} {item.Surname} {item.PhoneNumber}");
        }
    }
    private static List<PhoneBookItem>? Order(List<PhoneBookItem> phoneBookItems)
    {
        if (phoneBookItems.Count <= 1)
            return phoneBookItems;

        Console.WriteLine("Please choose an ordering to sort: “Ascending” or “Descending”.");
        var isAscending = Console.ReadLine()!.ToLower() == "ascending";
        Console.WriteLine();

        Console.WriteLine("Please choose criteria: “Name”, “Surname” or “PhoneNumberCode”.");
        var sortProperty = Console.ReadLine()!.ToLower();
        if (sortProperty != "name" && sortProperty != "surname" && sortProperty != "phonenumbercode")
        {
            Console.WriteLine("Invalid criteria");
            throw new InvalidOperationException();
        }
        List<PhoneBookItem> sorteditems = null;

        switch (sortProperty)
        {
            case "name":
                sorteditems = SortPhoneBookItems(phoneBookItems, x => x.Name, isAscending).ToList();
                break;
            case "surname":
                //sorteditems = SortPhoneBookItems(phoneBookItems, x => x.Surname, isAscending, new EmptyStringAtEndComparer()!).ToList();

                var itemsWithoutEmptySurnames = phoneBookItems.Where(x => !string.IsNullOrEmpty(x.Surname)).ToList();
                var itemsWithEmptySurnames = phoneBookItems.Where(x => string.IsNullOrEmpty(x.Surname));

                sorteditems = SortPhoneBookItems(itemsWithoutEmptySurnames, x => x.Surname, isAscending).ToList();
                sorteditems.AddRange(itemsWithEmptySurnames);
                break;
            case "phonenumbercode":
                sorteditems = SortPhoneBookItems(phoneBookItems, x => x.PhoneNumberCode, isAscending).ToList();
                break;
        }

        return sorteditems;

    }
    private static IOrderedEnumerable<PhoneBookItem> SortPhoneBookItems<TProperty>(List<PhoneBookItem> source, Func<PhoneBookItem, TProperty> propertySelector, bool isAscending, IComparer<TProperty>? comparer = null)
    {
        comparer ??= Comparer<TProperty>.Default;

        if (isAscending)
        {
            return source.OrderBy(propertySelector, comparer);
        }

        return source.OrderByDescending(propertySelector, comparer);
    }
}
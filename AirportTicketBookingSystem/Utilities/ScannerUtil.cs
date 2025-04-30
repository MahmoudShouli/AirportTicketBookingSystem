using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Printers;

namespace AirportTicketBookingSystem.Utilities;

public static class ScannerUtil
{
    public static int ScanInt(string prompt)
    {
        Console.WriteLine("Enter " + prompt);

        int result;
        while (true)
        {
            string? input = Console.ReadLine();

            if (int.TryParse(input, out result))
            {
                return result;
            }

            Console.WriteLine("Input must be a valid integer.");
        }
    }
    
    public static string ScanNonEmptyString(string prompt)
    {
        Console.WriteLine("Enter " + prompt);

        string? input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input must be a valid non-empty string.");
            input = Console.ReadLine();
        }

        return input;
    }

    public static decimal ScanDecimal(string prompt)
    {
        Console.WriteLine("Enter " + prompt);

        decimal result;
        while (true)
        {
            string? input = Console.ReadLine();

            if (decimal.TryParse(input, out result))
            {
                return result;
            }

            Console.WriteLine("Input must be a valid decimal.");
        }
        
        
    }

    public static DateTime ScanDate(string prompt)
    {
        Console.WriteLine("Enter " + prompt);

        DateTime result;
        while (true)
        {
            string? input = Console.ReadLine();

            if (DateTime.TryParse(input, out result))
            {
                return result;
            }

            Console.WriteLine("Input must be a valid date.");
        }
    }

    public static FlightClass ScanFlightClass(string prompt)
    {
        Console.WriteLine("Enter " + prompt);

        FlightClass result;
        while (true)
        {
            string? input = Console.ReadLine();

            if (Enum.TryParse<FlightClass>(input, ignoreCase: true, out result))
            {
                return result;
            }

            Console.WriteLine("Input must be a valid flight class.");
        }
    }
}
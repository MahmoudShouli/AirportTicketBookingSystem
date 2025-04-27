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

            MenuPrinter.PrintInvalidMessage("enter a valid integer.");
        }
    }


    public static string ScanNonEmptyString(string prompt)
    {
        Console.WriteLine("Enter " + prompt);

        string? input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input cannot be empty. Enter " + prompt);
            input = Console.ReadLine();
        }

        return input;
    }

}
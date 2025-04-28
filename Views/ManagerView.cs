using AirportTicketBookingSystem.launchers;
using AirportTicketBookingSystem.Printers;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Views;

public static class ManagerView
{
    public static void ImportHandler()
    {
        while (true)
        {
            MenuPrinter.PrintValidationDetails();
            
            var input = ScannerUtil.ScanNonEmptyString("your file path or Q to exit: ");
            
            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                return;
            
            var report = AppStartup.ManagerController.ImportFlights(input);

            // 1 means the report only contains the header (no errors added to it)
            if (report.Count == 1)
            {
                HelperPrinter.PrintAnyKeyMessage("Import success!");
                return;
            }
            
            report.ForEach(Console.WriteLine);
            HelperPrinter.PrintAnyKeyMessage();
        }
    }
    
    public static void FilterBookingsHandler()
    {
        while (true)
        {
            MenuPrinter.PrintFilterBookingsMenu();
            
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                
                case 10:
                    return;
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 10)");
                    break;
            }
        }
    }
}
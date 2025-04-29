using AirportTicketBookingSystem.launchers;
using AirportTicketBookingSystem.Models;
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
        var bookingFilter = new BookingFilter();
        
        while (true)
        {
            MenuPrinter.PrintFilterBookingsMenu();
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                case 1:
                    bookingFilter.All = true;
                    break;
                
                case 2:
                    bookingFilter.Price = ScannerUtil.ScanDecimal("price");
                    break;
                
                case 3:
                    bookingFilter.DepartureCountry = ScannerUtil.ScanNonEmptyString("departure country");
                    break;

                case 4:
                    bookingFilter.DestinationCountry = ScannerUtil.ScanNonEmptyString("destination country");
                    break;

                case 5:
                    bookingFilter.DepartureDate = ScannerUtil.ScanDate("departure date (yyyy-MM-dd)");
                    break;

                case 6:
                    bookingFilter.DepartureAirport = ScannerUtil.ScanNonEmptyString("departure airport");
                    break;

                case 7:
                    bookingFilter.DestinationAirport = ScannerUtil.ScanNonEmptyString("destination airport");
                    break;

                case 8:
                    bookingFilter.FlightClass = ScannerUtil.ScanFlightClass("flight class"); 
                    break;
                
                case 9:
                    bookingFilter.PassengerName = ScannerUtil.ScanNonEmptyString("passenger name");
                    break;
                
                case 10:
                    var filteredBookings = AppStartup.ManagerController.FilterBookings(bookingFilter);
                    HelperPrinter.PrintBookings(filteredBookings);
                    HelperPrinter.PrintAnyKeyMessage();
                    return;
                
                case 11:
                    return;
                
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 11)");
                    break;
            }
            
        }
    }
}
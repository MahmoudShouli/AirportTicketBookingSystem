using AirportTicketBookingSystem.launchers;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Printers;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Views;

public class PassengerView
{
    public static void SearchHandler()
    {
        var flightFilter = new FlightFilter();
        
        while (true)
        {
            MenuPrinter.PrintSearchFlightsMenu();
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                case 1:
                    flightFilter.All = true;
                    break;
                
                case 2:
                    flightFilter.Price = ScannerUtil.ScanDecimal("price");
                    break;
                
                case 3:
                    flightFilter.DepartureCountry = ScannerUtil.ScanNonEmptyString("departure country");
                    break;

                case 4:
                    flightFilter.DestinationCountry = ScannerUtil.ScanNonEmptyString("destination country");
                    break;

                case 5:
                    flightFilter.DepartureDate = ScannerUtil.ScanDate("departure date (yyyy-MM-dd)");
                    break;

                case 6:
                    flightFilter.DepartureAirport = ScannerUtil.ScanNonEmptyString("departure airport");
                    break;

                case 7:
                    flightFilter.DestinationAirport = ScannerUtil.ScanNonEmptyString("destination airport");
                    break;

                case 8:
                    flightFilter.FlightClass = ScannerUtil.ScanFlightClass("flight class"); 
                    break;
                
                case 9:
                    var filteredFlights = AppStartup.PassengerController.SearchFlights(flightFilter);
                    HelperPrinter.PrintFlights(filteredFlights);
                    HelperPrinter.PrintAnyKeyMessage();
                    return;
                
                case 10:
                    return;
                
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 10)");
                    break;
            }
            
        }
    }

    public static void BookHandler()
    {
        while (true)
        {
            Console.Clear();

            var input = ScannerUtil.ScanNonEmptyString("flight id");
            
            var success = AppStartup.PassengerController.BookFlight(input);

            if (success)
            {
                Console.WriteLine("Flight booked successfully");
                
                
            }
            HelperPrinter.PrintAnyKeyMessage();
            return;
        }
    }
}
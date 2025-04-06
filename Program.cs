using AirportTicketBookingSystem.Repository;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Utils;

namespace AirportTicketBookingSystem;

internal static class Program
{
    private static void Main()
    {
        var flightRepo = new FlightRepository();
        var flightService = new FlightService(flightRepo);
        var passengerService = new PassengerServices(flightService);
        var printer = new Printer(passengerService);
        
        printer.ShowMainMenu();
    }
}
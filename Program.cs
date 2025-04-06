using AirportTicketBookingSystem.Repository;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Utils;

namespace AirportTicketBookingSystem;

internal static class Program
{
    private static void Main()
    {
        var flightRepo = new FlightRepository();
        var passengerRepo = new PassengersRepository();
        
        var flightService = new FlightService(flightRepo);
        var passengerService = new PassengerServices(passengerRepo);
        
        var printer = new Printer(passengerService, flightService);
        
        printer.ShowMainMenu();
    }
}
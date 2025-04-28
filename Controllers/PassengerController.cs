using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Controllers;

public class PassengerController(IPassengerServices passengerServices)
{
    public List<Flight> SearchFlights(FlightFilter flightFilter)
    {
        var flights = new List<Flight>();
        try
        {
            flights = passengerServices.SearchFlights(flightFilter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return flights;
    }
}
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

    public bool BookFlight(string flightId)
    {
        try
        {
            passengerServices.BookFlight(flightId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool CancelBooking(string flightId)
    {
        try
        {
            passengerServices.CancelBooking(flightId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
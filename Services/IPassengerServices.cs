using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IPassengerServices 
{
    List<Flight> SearchFlights(FlightFilter flightFilter);
    void BookFlight(string flightId);
}
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IPassengerServices 
{
    List<Flight> SearchFlights(FlightFilter flightFilter);
    Booking BookFlight(string flightId);
    void CancelBooking(string flightId);
}
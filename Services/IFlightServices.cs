using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IFlightServices
{
    public List<Flight> SearchFlights(bool isBooked, string? keyword = null, decimal? price = null,
        DateTime? date = null, Class? flightClass = null);
    public Booking? BookFlight(Passenger passenger, string flightId);
    
}
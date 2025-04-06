using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services;

public class PassengerServices(FlightService flightService)
{
    public List<Flight> SearchFlightsForPassengers(string? keyword = null, decimal? price = null, DateTime? date  = null, Class? flightClass = null)
    {
        return flightService.SearchFlights(keyword, price, date, flightClass);
    }
}
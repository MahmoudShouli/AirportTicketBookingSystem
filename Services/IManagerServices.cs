using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IManagerServices
{
    public List<Booking> FilterBookings(string? keyword = null, decimal? price = null, DateTime? date = null,
        FlightClass? flightClass = null, string? passenger = null);
}
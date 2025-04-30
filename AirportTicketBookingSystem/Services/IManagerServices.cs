using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IManagerServices
{
    List<string> ImportFlights(string filePath);
    List<Booking> FilterBookings(BookingFilter filter);
}
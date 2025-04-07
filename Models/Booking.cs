using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class Booking
{
    public string PassengerName { get; set; }
    public string FlightId { get; set; }

    public override string ToString()
    {
        return $"Booking for flight {FlightId} for Passenger {PassengerName}";
    }
}
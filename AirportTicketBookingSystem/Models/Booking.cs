namespace AirportTicketBookingSystem.Models;

public class Booking
{
    public string PassengerName { get; init; } = "";
    public string FlightId { get; init; } = "";

    public override string ToString()
    {
        return $"Booking for flight {FlightId} for Passenger {PassengerName}";
    }
}
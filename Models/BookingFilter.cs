namespace AirportTicketBookingSystem.Models;

public class BookingFilter : FlightFilter
{
    public string? PassengerName { get; set; }

    public override bool IsEmpty()
    {
        return base.IsEmpty() && string.IsNullOrEmpty(PassengerName);
    }
}
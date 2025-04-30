namespace AirportTicketBookingSystem.Exceptions;

public class FlightAlreadyBookedException : Exception
{
    public FlightAlreadyBookedException()
        : base("Flight is already booked") {}
}
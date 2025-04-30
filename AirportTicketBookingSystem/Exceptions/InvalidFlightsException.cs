namespace AirportTicketBookingSystem.Exceptions;

public class InvalidFlightsException : Exception
{
    public InvalidFlightsException(string message)
        : base(message) {}
}
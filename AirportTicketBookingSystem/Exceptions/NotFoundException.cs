namespace AirportTicketBookingSystem.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string prompt)
        : base($"{prompt} not found.") {}
}
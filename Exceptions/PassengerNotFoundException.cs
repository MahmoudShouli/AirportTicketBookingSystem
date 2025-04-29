namespace AirportTicketBookingSystem.Exceptions;

public class PassengerNotFoundException : Exception
{
    public PassengerNotFoundException(string name)
        : base($"Passenger '{name}' not found.") {}
}
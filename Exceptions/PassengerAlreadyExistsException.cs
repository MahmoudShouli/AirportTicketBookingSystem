namespace AirportTicketBookingSystem.Exceptions;

public class PassengerAlreadyExistsException : Exception
{
    public PassengerAlreadyExistsException(string name)
        : base($"Passenger '{name}' already exists.") {}
}
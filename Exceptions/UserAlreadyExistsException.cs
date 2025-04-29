namespace AirportTicketBookingSystem.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string name)
        : base($"User '{name}' already exists.") {}
}
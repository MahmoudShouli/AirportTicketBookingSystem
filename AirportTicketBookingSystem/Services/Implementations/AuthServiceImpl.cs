using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;

namespace AirportTicketBookingSystem.Services.Implementations;

public class AuthServiceImpl(IPassengersRepository passengersRepository) : IAuthService
{
    public User Login(string name, string password)
    {
        if (IsAdmin(name, password))
        {
            var manager = new Manager (name, password);
            UserContext.SetCurrentUser(manager);
            
            return manager;
        }
        
        var passenger = passengersRepository.SearchPassengerByName(name);

        if (passenger == null)
        {
            throw new NotFoundException($"Passenger with name {name}");
        }

        if (!password.Equals(passenger.Password))
        {
            throw new InvalidPasswordException();
        }
            
        UserContext.SetCurrentUser(passenger);
        return passenger;
    }

    public void Register(string name, string password)
    {
        var passenger = passengersRepository.SearchPassengerByName(name);

        if (passenger != null || IsAdmin(name, password))
        {
            throw new UserAlreadyExistsException(name);
        }

        var newPassenger = new Passenger(name, password);
        passengersRepository.AddPassenger(newPassenger);
        
        UserContext.SetCurrentUser(newPassenger);
    }

    private bool IsAdmin(string name, string password)
    {
        // hardcoded manager
        return (name.Equals("Mahmoud", StringComparison.OrdinalIgnoreCase) && password.Equals("123"));
    }
}
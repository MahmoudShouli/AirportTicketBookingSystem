using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;

namespace AirportTicketBookingSystem.Services.Implementations;

public class AuthServiceImpl(IPassengersRepository passengersRepository) : IAuthService
{
    public void Login(string name, string password)
    {
        // hardcoded manager
        if (name.Equals("Mahmoud", StringComparison.OrdinalIgnoreCase) && password.Equals("123"))
        {
            var manager = new Manager (name, password);
            
            UserContext.SetCurrentUser(manager);
        }
        
        var passenger = passengersRepository.SearchPassengerByName(name);

        if (passenger == null)
        {
            throw new PassengerNotFoundException(name);
        }

        if (!password.Equals(passenger.Password))
        {
            throw new InvalidPasswordException();
        }
            
        UserContext.SetCurrentUser(passenger);
    }

    public void Register(string name, string password)
    {
        var passenger = passengersRepository.SearchPassengerByName(name);

        if (passenger != null)
        {
            throw new PassengerAlreadyExistsException(name);
        }
        
        var newPassenger = new Passenger(name, password);
        passengersRepository.AddPassenger(newPassenger);
    }
}
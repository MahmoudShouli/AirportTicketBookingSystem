using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services;

public class PassengerServices(IPassengersRepository passengersRepository)
{
    public Passenger? AuthenticatePassenger(string name)
    {
        return passengersRepository.GetPassengerByName(name);
    }

    public void RegisterPassenger(Passenger passenger)
    {
        passengersRepository.AddPassenger(passenger);
    }
    
}
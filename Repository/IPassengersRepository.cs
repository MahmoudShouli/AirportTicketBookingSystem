using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repository;

public interface IPassengersRepository
{
    Passenger? GetPassengerByName(string passengerName);
    
    void AddPassenger(Passenger passenger);
}
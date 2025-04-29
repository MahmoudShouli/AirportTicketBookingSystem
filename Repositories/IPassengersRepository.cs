using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repositories;

// Data Access for passengers
public interface IPassengersRepository
{
    List<Passenger> GetAllPassengers();
    Passenger? SearchPassengerByName(string passengerName);
    void SavePassengers(List<Passenger> passengers);
    void AddPassenger(Passenger passenger);
    void Update();
}
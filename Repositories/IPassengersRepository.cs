using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repositories;

public interface IPassengersRepository
{
    List<Passenger> GetAllPassengers();
    Passenger? SearchPassengerByName(string passengerName);
    void SavePassengers(List<Passenger> passengers);
    void AddPassenger(Passenger passenger);
}
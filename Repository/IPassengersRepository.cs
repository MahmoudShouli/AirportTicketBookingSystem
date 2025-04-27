using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repository;

public interface IPassengersRepository
{
    List<Passenger> GetAllPassengers();
    Passenger? SearchPassengerByName(string passengerName);
    void SavePassengers(List<Passenger> passengers);
}
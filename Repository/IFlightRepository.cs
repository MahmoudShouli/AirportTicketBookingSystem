using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repository;

public interface IFlightRepository
{
     List<Flight> LoadFlights();
}
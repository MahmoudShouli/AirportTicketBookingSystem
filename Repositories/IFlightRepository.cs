using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repositories;

// Data Access for flights
public interface IFlightRepository
{
     List<Flight> GetAllFlights();
     Flight? GetFlightById(string id);
     List<Flight> GetFlightsByAvailability(bool isAvailable);
     List<Flight> SearchFlights(FlightFilter filter);
     void SaveFlights(List<Flight> flights);
     void Update();


}
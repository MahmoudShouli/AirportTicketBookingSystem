using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repositories;

public interface IFlightRepository
{
     List<Flight> GetAllFlights();
     List<Flight> SearchFlights(FlightFilter filter);
     void SaveFlights(List<Flight> flights);
    
     
}
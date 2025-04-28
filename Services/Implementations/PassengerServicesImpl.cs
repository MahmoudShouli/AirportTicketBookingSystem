using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;

namespace AirportTicketBookingSystem.Services.Implementations;

public class PassengerServicesImpl(IFlightRepository flightRepository) : IPassengerServices
{
    public List<Flight> SearchFlights(FlightFilter flightFilter)
    {
        if (flightFilter.IsEmpty())
        {
            throw new ArgumentException("Choose filters first");
        }
        return flightFilter.All ?  flightRepository.GetAllFlights() : flightRepository.SearchFlights(flightFilter);
    }
}
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services;

public class PassengerServices(FlightService flightService, IPassengersRepository passengersRepository)
{
    public List<Flight> SearchFlightsForPassengers(string? keyword = null, decimal? price = null, DateTime? date  = null, Class? flightClass = null)
    {
        return flightService.SearchFlights(keyword, price, date, flightClass);
    }

    public Passenger? AuthenticatePassenger(string name)
    {
        return passengersRepository.GetPassengerByName(name);
    }

    public void RegisterPassenger(Passenger passenger)
    {
        passengersRepository.AddPassenger(passenger);
    }
}
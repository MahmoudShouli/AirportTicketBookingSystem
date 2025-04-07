using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services;

public class ManagerServices(FlightService flightService, IPassengersRepository passengersRepository)
{
    public List<Booking> FilterBookings(string? keyword = null, decimal? price = null, DateTime? date = null, Class? flightClass = null, string? passenger = null)
    {
        if (passenger != null)
        {
            return passengersRepository.GetPassengerByName(passenger)!.Bookings;
        }
        
        var flights = flightService.SearchFlights(true, keyword, price, date, flightClass);
        var flightIDs = flights.Select(f=> f.Id).ToList();
        var allPassengers = passengersRepository.GetAllPassengers();
        var allBookings = allPassengers.SelectMany(p => p.Bookings).ToList();
        
        return allBookings.Where(b => flightIDs.Contains(b.FlightId)).ToList();
        
    }
}
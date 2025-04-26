using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services.Implementations;

public class FlightService(IFlightRepository flightRepository) : IFlightServices
{
    public List<Flight> SearchFlights(bool isBooked, string? keyword = null, decimal? price = null, DateTime? date = null, Class? flightClass = null) 
    {
        var flights = flightRepository.LoadFlights().Where(flight => flight.IsBooked == isBooked).ToList();
        var filteredFlights = new List<Flight>();
        
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var lowerKeyword = keyword.ToLower();
            
            if (lowerKeyword == "all")
                return flights;
            
            filteredFlights = flights.Where(f =>
                f.Id.ToLower().Equals(lowerKeyword, StringComparison.OrdinalIgnoreCase)||
                f.DepartureCountry.ToLower().Equals(lowerKeyword, StringComparison.OrdinalIgnoreCase) ||
                f.DestinationCountry.ToLower().Equals(lowerKeyword, StringComparison.OrdinalIgnoreCase) ||
                f.DepartureAirport.ToLower().Equals(lowerKeyword, StringComparison.OrdinalIgnoreCase) ||
                f.DestinationAirport.ToLower().Equals(lowerKeyword, StringComparison.OrdinalIgnoreCase) 
            ).ToList();
        }
        
        else if (flightClass.HasValue)
        {
            filteredFlights = flights.Where(f => f.Class == flightClass.Value).ToList();
        }
        
        else if (price.HasValue)
        {
            filteredFlights = flights.Where(f => f.Price == price.Value).ToList();
        }

        else if (date.HasValue)
        {
            filteredFlights = flights.Where(f => f.DepartureDate.Date == date.Value.Date).ToList();
        }

        return filteredFlights;
    }
    
    public Booking? BookFlight(Passenger passenger, string flightId)
    {
        var flights = flightRepository.LoadFlights();
        var flightToBook = flights.FirstOrDefault(f=> f.Id == flightId);

        if (flightToBook != null)
        {
            if (!flightToBook.IsBooked)
            {
                var newBooking = new Booking { FlightId = flightId, PassengerName = passenger.Name };
                passenger.AddBooking(newBooking);
                flightToBook.IsBooked = true;
                flightRepository.SaveFlights(flights);
                return newBooking;
            }
            
        }

        return null;
    }
}

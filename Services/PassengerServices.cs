using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services;

public class PassengerServices
{
    private static FlightRepository FlightRepository { get; set; } = new FlightRepository();
    
    public static List<Flight> SearchForFlights(string? keyword = null, decimal? price = null, DateTime? date  = null, Class? flightClass = null)
    {

        var flights = FlightRepository.LoadData();
        var filteredFlights = new List<Flight>();

        
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            
            var lowerKeyword = keyword.ToLower();
            
            if (lowerKeyword == "all")
                return flights;
            
            filteredFlights = flights.Where(f =>
                f.Id.ToLower().Equals(lowerKeyword)||
                f.DepartureCountry.ToLower().Equals(lowerKeyword) ||
                f.DestinationCountry.ToLower().Equals(lowerKeyword) ||
                f.DepartureAirport.ToLower().Equals(lowerKeyword) ||
                f.DestinationAirport.ToLower().Equals(lowerKeyword) 
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
}
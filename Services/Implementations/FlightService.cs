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
    
    public bool ValidateAllFlights(List<Flight> flights)
    {
        var seenIds = new HashSet<string>();
        var isValid = true;

        Console.WriteLine("\nValidation Report:\n");

        foreach (var flight in flights)
        {
            var errors = ValidateSingleFlight(flight, seenIds);
            if (errors.Count > 0)
            {
                isValid = false;
                Console.WriteLine($"Flight {flight.Id ?? "[no ID]"} is invalid because:");
                errors.ForEach(error => Console.WriteLine($"   - {error}"));
                Console.WriteLine();
            }
        }

        return isValid;
    }

    private List<string> ValidateSingleFlight(Flight flight, HashSet<string> seenIds)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(flight.Id))
            errors.Add("Flight ID is required.");
        else if (!seenIds.Add(flight.Id))
            errors.Add("Flight ID must be unique.");

        if (flight.Price <= 0)
            errors.Add("Price must be greater than 0.");

        if (string.IsNullOrWhiteSpace(flight.DepartureCountry))
            errors.Add("Departure Country is required.");

        if (string.IsNullOrWhiteSpace(flight.DestinationCountry))
            errors.Add("Destination Country is required.");
        else if (flight.DestinationCountry.Equals(flight.DepartureCountry, StringComparison.OrdinalIgnoreCase))
            errors.Add("Destination Country cannot be the same as Departure Country.");

        if (flight.DepartureDate <= DateTime.Now)
            errors.Add("Departure Date must be in the future.");

        if (string.IsNullOrWhiteSpace(flight.DepartureAirport))
            errors.Add("Departure Airport is required.");

        if (string.IsNullOrWhiteSpace(flight.DestinationAirport))
            errors.Add("Destination Airport is required.");
        else if (flight.DestinationAirport.Equals(flight.DepartureAirport, StringComparison.OrdinalIgnoreCase))
            errors.Add("Destination Airport cannot be the same as Departure Airport.");

        return errors;
    }
}

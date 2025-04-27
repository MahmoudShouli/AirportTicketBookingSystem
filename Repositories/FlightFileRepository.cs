using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Repositories;

public class FlightFileRepository : IFlightRepository
{
    private readonly string _filePath;
    private List<Flight> _flights;

    public FlightFileRepository(string filePath)
    {
        _filePath = filePath;
        _flights = FileUtilities.ConvertFileToList<Flight>(filePath, parts => new Flight
        {
            Id = parts[0],
            Price = decimal.Parse(parts[1]),
            DepartureCountry = parts[2],
            DestinationCountry = parts[3],
            DepartureDate = DateTime.Parse(parts[4]),
            DepartureAirport = parts[5],
            DestinationAirport = parts[6],
            FlightClass = (FlightClass)Enum.Parse(typeof(FlightClass), parts[7], ignoreCase: true),
            IsBooked = bool.Parse(parts[8])
        });
    }

    public List<Flight> GetAllFlights() => _flights;

    public List<Flight> SearchFlights(FlightFilter filter)
    {
        var query = _flights.AsQueryable();

        if (filter.Price.HasValue)
        {
            var targetPrice = filter.Price.Value;
            query = query.Where(f => f.Price >= targetPrice - 10 && f.Price <= targetPrice + 10);
        }
        
        if (!string.IsNullOrWhiteSpace(filter.DepartureCountry))
            query = query.Where(f => f.DepartureCountry.Equals(filter.DepartureCountry, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.DestinationCountry))
            query = query.Where(f => f.DestinationCountry.Equals(filter.DestinationCountry, StringComparison.OrdinalIgnoreCase));
        
        if (filter.DepartureDate.HasValue)
            query = query.Where(f => f.DepartureDate.Date == filter.DepartureDate.Value.Date);
        
        if (!string.IsNullOrWhiteSpace(filter.DepartureAirport))
            query = query.Where(f => f.DepartureAirport.Equals(filter.DepartureAirport, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.DestinationAirport))
            query = query.Where(f => f.DestinationAirport.Equals(filter.DestinationAirport, StringComparison.OrdinalIgnoreCase));

        if (filter.FlightClass.HasValue)
            query = query.Where(f => f.FlightClass == filter.FlightClass.Value);

        return query.ToList();
    }


    public void SaveFlights(List<Flight> flights)
    {
        _flights = flights;
        
        var lines = new List<string> { "FlightId,Price,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,DestinationAirport,Class,IsBooked" }
            .Concat(flights.Select(flight => 
                $"{flight.Id},{flight.Price},{flight.DepartureCountry},{flight.DestinationCountry}," +
                $"{flight.DepartureDate:yyyy-MM-ddTHH:mm:ss},{flight.DepartureAirport},{flight.DestinationAirport}," +
                $"{flight.FlightClass},{flight.IsBooked}"
            ))
            .ToList();

        
        File.WriteAllLines(_filePath, lines);
    }
}
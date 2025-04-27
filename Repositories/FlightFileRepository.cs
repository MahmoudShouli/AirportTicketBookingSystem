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
        _flights = FileUtil.ConvertFileToList(filePath, CsvUtil.CsvToFlight);
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
        var lines = CsvUtil.FlightsToCsv(flights);
        File.WriteAllLines(_filePath, lines);
    }
}
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
    
    public Flight? GetFlightById(string id)
    {
       return _flights.FirstOrDefault(flight => flight.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
    }

    public List<Flight> GetFlightsByAvailability(bool isAvailable)
    {
        return _flights.Where(f=> f.IsBooked == !isAvailable).ToList();
    }


    public List<Flight> SearchFlights(FlightFilter filter)
    {
        return _flights.Where(f =>
            (!filter.Price.HasValue || (f.Price >= filter.Price.Value - 10 && f.Price <= filter.Price.Value + 10)) &&
            (string.IsNullOrWhiteSpace(filter.DepartureCountry) || f.DepartureCountry.Equals(filter.DepartureCountry, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(filter.DestinationCountry) || f.DestinationCountry.Equals(filter.DestinationCountry, StringComparison.OrdinalIgnoreCase)) &&
            (!filter.DepartureDate.HasValue || f.DepartureDate.Date == filter.DepartureDate.Value.Date) &&
            (string.IsNullOrWhiteSpace(filter.DepartureAirport) || f.DepartureAirport.Equals(filter.DepartureAirport, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(filter.DestinationAirport) || f.DestinationAirport.Equals(filter.DestinationAirport, StringComparison.OrdinalIgnoreCase)) &&
            (!filter.FlightClass.HasValue || f.FlightClass == filter.FlightClass.Value) 
        ).ToList();
    }
    
    public void SaveFlights(List<Flight> flights)
    {
        _flights = flights;
        var lines = CsvUtil.FlightsToCsv(flights);
        File.WriteAllLines(_filePath, lines);
    }

    public void Update()
    {
        SaveFlights(_flights);
    }
}
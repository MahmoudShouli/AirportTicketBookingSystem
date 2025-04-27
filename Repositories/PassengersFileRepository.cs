using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Repositories;

public class PassengersFileRepository : IPassengersRepository
{
    private readonly string _filePath;
    private List<Passenger> _passengers;

    public PassengersFileRepository(string filePath)
    {
        _filePath = filePath;
        _passengers = FileUtil.ConvertFileToList<Passenger>(filePath, parts => new Passenger
        {
            Name = parts[0],
            Password = parts[1],
            Bookings = string.IsNullOrWhiteSpace(parts[2])
                ? new List<Booking>()
                : parts[2].Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(flightId => new Booking { PassengerName = parts[0], FlightId = flightId })
                    .ToList()
        });
    }

    public List<Passenger> GetAllPassengers() => _passengers;

    public Passenger? SearchPassengerByName(string name)
    {
        return _passengers.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void SavePassengers(List<Passenger> passengers)
    { 
        _passengers = passengers;
        
        var lines = new List<string> { "Name,Password,Bookings" }
            .Concat(passengers.Select(passenger =>
                $"{passenger.Name},{passenger.Password}," +
                $"{string.Join(';', passenger.Bookings.Select(b => b.FlightId))}"
            ))
            .ToList();
                
        File.WriteAllLines(_filePath, lines);
    }
}



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
        _passengers = FileUtil.ConvertFileToList(filePath, CsvUtil.CsvToPassenger);
    }

    public List<Passenger> GetAllPassengers() => _passengers;

    public Passenger? SearchPassengerByName(string name)
    {
        return _passengers.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void SavePassengers(List<Passenger> passengers)
    { 
        _passengers = passengers;
        var lines = CsvUtil.PassengersToCsv(passengers);
        File.WriteAllLines(_filePath, lines);
    }

    public void AddPassenger(Passenger passenger)
    {
        _passengers.Add(passenger);
        SavePassengers(_passengers);
    }
}



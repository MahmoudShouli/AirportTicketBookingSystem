using System.ComponentModel.Design;
using AirportTicketBookingSystem.Models;
using System.Globalization;

namespace AirportTicketBookingSystem.Data;

public class FileDataStore : IDataStore<Flight> 
{
    private readonly string _filePath;

    public FileDataStore(string filePath)
    {
        _filePath = filePath;
    }

    public List<Flight> LoadData()
    {
        var flights = new List<Flight>();

        if (!File.Exists(_filePath))
            return flights;

        var lines = File.ReadAllLines(_filePath);

        return lines
            .Skip(1) 
            .Select(line =>
            {
                var parts = line.Split(',');

                return new Flight
                {
                    Id = parts[0],
                    Price = decimal.Parse(parts[1]),
                    DepartureCountry = parts[2],
                    DestinationCountry = parts[3],
                    DepartureDate = DateTime.Parse(parts[4]),
                    DepartureAirport = parts[5],
                    DestinationAirport = parts[6]
                };
            })
            .ToList();
    }
}
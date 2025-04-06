using System.ComponentModel.Design;
using AirportTicketBookingSystem.Models;
using System.Globalization;
using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Repository;

public class FlightRepository : IFlightRepository
{
    private const string FilePath = @"D:\Computer Engineering\5th Year 2nd Sem\Internship\AirportTicketBookingSystem\Data\Flights.csv";

    public List<Flight> LoadFlights()
    {
        var flights = new List<Flight>();

        if (!File.Exists(FilePath))
            return flights;

        var lines = File.ReadAllLines(FilePath);

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
                    DestinationAirport = parts[6],
                    Class = (Class)Enum.Parse(typeof(Class), parts[7], ignoreCase: true),
                    IsBooked = bool.Parse(parts[8])

                };
            })
            .ToList();
    }

    public void SaveFlights(List<Flight> flights)
    {
        var lines = new List<string>
        {
            "FlightId,Price,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,DestinationAirport,Class,IsBooked"
        };

        foreach (var flight in flights)
        {
            var line = $"{flight.Id},{flight.Price},{flight.DepartureCountry},{flight.DestinationCountry}," +
                       $"{flight.DepartureDate:yyyy-MM-ddTHH:mm:ss},{flight.DepartureAirport},{flight.DestinationAirport}," +
                       $"{flight.Class},{flight.IsBooked}";
            lines.Add(line);
        }

        File.WriteAllLines(FilePath, lines);
    }
}
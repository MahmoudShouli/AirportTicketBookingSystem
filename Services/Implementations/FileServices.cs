using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services.Implementations;

public static class FileServices
{
    public static List<Flight> ConvertFileToFlights(string path)
    {
        try
        {
            var lines = File.ReadAllLines(path);

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
                        FlightClass = (FlightClass)Enum.Parse(typeof(FlightClass), parts[7], ignoreCase: true),
                        IsBooked = bool.Parse(parts[8])

                    };
                })
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error importing file: " + ex.Message);
            return new List<Flight>();
        }
    }
    
    public static void SaveImportedFile(string sourcePath)
    {
        
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        var dataDirectory = Path.Combine(projectRoot, "Data");
        
        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }
        
        var destinationPath = Path.Combine(dataDirectory, "Flights.csv");
        
        File.Copy(sourcePath, destinationPath, overwrite: true);

        Console.WriteLine("File imported successfully to the Data directory");
    }
}
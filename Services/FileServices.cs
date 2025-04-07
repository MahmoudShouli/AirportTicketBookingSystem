using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public static class FileServices
{
    public static List<Flight> ConvertFileToFlights(string path)
    {
        try
        {
            var flights = new List<Flight>();
            
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
                        Class = (Class)Enum.Parse(typeof(Class), parts[7], ignoreCase: true),
                        IsBooked = bool.Parse(parts[8])

                    };
                })
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error importing file: " + ex.Message);
            return null;
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
    public static bool Validate(List<Flight> flights)
    {
        var seenIds = new HashSet<string>();
        var hasErrors = false;

        Console.WriteLine("\nValidation Report:\n");

        foreach (var flight in flights)
        {
            var errors = new List<string>();

            // Flight ID
            if (string.IsNullOrWhiteSpace(flight.Id))
                errors.Add("Flight ID is required.");
            else if (!seenIds.Add(flight.Id))
                errors.Add("Flight ID must be unique.");

            // Price
            if (flight.Price <= 0)
                errors.Add("Price must be greater than 0.");

            // Departure Country
            if (string.IsNullOrWhiteSpace(flight.DepartureCountry))
                errors.Add("Departure Country is required.");

            // Destination Country
            if (string.IsNullOrWhiteSpace(flight.DestinationCountry))
                errors.Add("Destination Country is required.");
            else if (flight.DestinationCountry.Equals(flight.DepartureCountry, StringComparison.OrdinalIgnoreCase))
                errors.Add("Destination Country cannot be the same as Departure Country.");

            // Departure Date
            if (flight.DepartureDate <= DateTime.Now)
                errors.Add("Departure Date must be in the future.");

            // Departure Airport
            if (string.IsNullOrWhiteSpace(flight.DepartureAirport))
                errors.Add("Departure Airport is required.");

            // Destination Airport
            if (string.IsNullOrWhiteSpace(flight.DestinationAirport))
                errors.Add("Destination Airport is required.");
            else if (flight.DestinationAirport.Equals(flight.DepartureAirport, StringComparison.OrdinalIgnoreCase))
                errors.Add("Destination Airport cannot be the same as Departure Airport.");
            
            if (errors.Count > 0)
            {
                hasErrors = true;
                Console.WriteLine($"Flight {flight.Id} is invalid because:");
                errors.ForEach(e => Console.WriteLine($"   - {e}"));
                Console.WriteLine();
            }
        }

        return !hasErrors;
    }


}
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
                        Class = (Class)Enum.Parse(typeof(Class), parts[7], ignoreCase: true),
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
    
    public static bool ValidateAllFlights(List<Flight> flights)
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

    private static List<string> ValidateSingleFlight(Flight flight, HashSet<string> seenIds)
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
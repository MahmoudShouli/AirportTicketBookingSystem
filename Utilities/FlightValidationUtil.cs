using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilities;

public static class FlightValidationUtil
{
    public static List<string> ValidateAllFlights(List<Flight> flights)
    {
        var seenIds = new HashSet<string>();
        var report = new List<string> {"\nValidation Report:\n"};
        
        foreach (var flight in flights)
        {
            var errors = ValidateSingleFlight(flight, seenIds);
            if (errors.Count > 0)
            {
                report.Add($"Flight {flight.Id ?? "[no ID]"} is invalid because:");
                errors.ForEach(error => report.Add($"   - {error}"));
                report.Add("");
            }
        }
        
        return report;
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
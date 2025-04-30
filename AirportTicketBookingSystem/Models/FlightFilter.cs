using System.Runtime.CompilerServices;
using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class FlightFilter
{
    public bool All { get; set; } = false;
    public decimal? Price { get; set; }
    public string? DepartureCountry { get; set; }
    public string? DestinationCountry { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string? DepartureAirport { get; set; }
    public string? DestinationAirport { get; set; }
    public FlightClass? FlightClass { get; set; }

    public virtual bool IsEmpty()
    {
        return !All
               && !Price.HasValue
               && string.IsNullOrWhiteSpace(DepartureCountry)
               && string.IsNullOrWhiteSpace(DestinationCountry)
               && !DepartureDate.HasValue
               && string.IsNullOrWhiteSpace(DepartureAirport)
               && string.IsNullOrWhiteSpace(DestinationAirport)
               && !FlightClass.HasValue;
    }
}

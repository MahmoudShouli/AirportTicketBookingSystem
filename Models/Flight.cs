using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class Flight
{
    public string Id { get; init; } = "";
    public decimal Price { get; init; }
    public string DepartureCountry { get; init; } = "";
    public string DestinationCountry { get; init; } = "";
    public DateTime DepartureDate { get; init; }
    public string DepartureAirport { get; init; } = "";
    public string DestinationAirport { get; init; } = "";
    public Class Class { get; init; }
    public bool IsBooked { get; set; } = false;

    public override string ToString()
    {
        return $"Flight {Id} | " +
               $"Price: {Price:C} | " +
               $"From: {DepartureCountry} ({DepartureAirport}) -> " +
               $"To: {DestinationCountry} ({DestinationAirport}) | " +
               $"Date: {DepartureDate:yyyy-MM-dd} | " +
               $"Class: {Class}";
    }

}
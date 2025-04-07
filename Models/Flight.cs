using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class Flight
{
    public string Id { get; set; }
    public decimal Price { get; set; }
    public string DepartureCountry { get; set; }
    public string DestinationCountry { get; set; }
    public DateTime DepartureDate { get; set; }
    public string DepartureAirport { get; set; }
    public string DestinationAirport { get; set; }
    public Class Class { get; set; }
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
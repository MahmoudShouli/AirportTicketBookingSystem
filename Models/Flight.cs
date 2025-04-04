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
}
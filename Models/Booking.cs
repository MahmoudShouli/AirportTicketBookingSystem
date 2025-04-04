using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class Booking
{
    public string PassengerName { get; set; }
    public int FlightId { get; set; }
    public Class FlightClass { get; set; }
}
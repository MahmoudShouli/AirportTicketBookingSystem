namespace AirportTicketBookingSystem.Models;


public class Passenger
{
    public string Name { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}
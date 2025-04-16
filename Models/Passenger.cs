using System.Collections;

namespace AirportTicketBookingSystem.Models;

#nullable disable
public class Passenger
{
    public string Name { get; init; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();

    public void AddBooking(Booking booking)
    {
        Bookings.Add(booking);
    }

    public void RemoveBooking(Booking booking)
    {
        Bookings.Remove(booking);
    }
}
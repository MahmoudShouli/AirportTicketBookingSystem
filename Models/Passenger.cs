namespace AirportTicketBookingSystem.Models;


public class Passenger
{
    public string Name { get; set; }
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
using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;


public class Passenger : User
{
    public List<Booking> Bookings { get; set; }

    public Passenger()
    {
        Bookings = new List<Booking>();
        UserType = UserType.Passenger;
    }
    public Passenger(string name, string password)
    {
        Name = name;
        Password = password;
        Bookings = new List<Booking>();
        UserType = UserType.Passenger;
    }

    public void AddBooking(Booking booking)
    {
        Bookings.Add(booking);
    }

    public void RemoveBooking(Booking booking)
    {
        Bookings.Remove(booking);
    }
}
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IPassengerServices 
{
    public Passenger? AuthenticatePassenger(string name);
    public void RegisterPassenger(Passenger passenger);
    public string CancelBooking(Passenger passenger, string flightId);
}
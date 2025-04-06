using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repository;

namespace AirportTicketBookingSystem.Services;

public class PassengerServices(IPassengersRepository passengersRepository)
{
    public Passenger? AuthenticatePassenger(string name)
    {
        return passengersRepository.GetPassengerByName(name);
    }

    public void RegisterPassenger(Passenger passenger)
    {
        passengersRepository.AddPassenger(passenger);
    }

    public string CancelBooking(Passenger passenger, string flightId)
    {
        var booking = passenger.Bookings.FirstOrDefault(b => b.FlightId == flightId);

        if (booking == null)
        {
            return "Booking not found ";
        }
        
        
        passenger.RemoveBooking(booking);
        return "Booking has been cancelled ";
        
        
    }
    
}
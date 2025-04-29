using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;

namespace AirportTicketBookingSystem.Services.Implementations;

public class PassengerServicesImpl(IPassengersRepository passengersRepository, IFlightRepository flightRepository) : IPassengerServices
{
    public List<Flight> SearchFlights(FlightFilter flightFilter)
    {
        if (flightFilter.IsEmpty())
        {
            throw new ArgumentException("Choose filters first");
        }
        return flightFilter.All ?  flightRepository.GetFlightsByAvailability(true): flightRepository.SearchFlights(flightFilter);
    }

    public void BookFlight(string flightId)
    {
        var flightToBook = flightRepository.GetFlightById(flightId);

        if (flightToBook == null)
        {
            throw new ArgumentException("Flight not found");
        }

        if (flightToBook.IsBooked)
        {
            throw new ArgumentException("Flight is already booked");
        }

        var currentUser = UserContext.CurrentUser as Passenger;
        var booking = new Booking { FlightId = flightId, PassengerName = currentUser!.Name };
        
        currentUser.AddBooking(booking);
        flightToBook.IsBooked = true;
        
        flightRepository.Update();
        passengersRepository.Update();
    }

    public void CancelBooking(string flightId)
    {
        var flightToCancel = flightRepository.GetFlightById(flightId);
        
        if (flightToCancel == null)
        {
            throw new ArgumentException("Flight not found");
        }
        
        var currentUser = UserContext.CurrentUser as Passenger;
        var bookingToCancel = currentUser!.Bookings.FirstOrDefault(b => b.FlightId == flightToCancel.Id);
        
        if (bookingToCancel == null)
        {
            throw new ArgumentException("Booking not found");
        }
        
        currentUser.RemoveBooking(bookingToCancel);
        flightToCancel.IsBooked = false;
        
        flightRepository.Update();
        passengersRepository.Update();
    }
}
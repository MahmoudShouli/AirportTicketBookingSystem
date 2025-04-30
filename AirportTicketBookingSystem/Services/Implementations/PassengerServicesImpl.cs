using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;

namespace AirportTicketBookingSystem.Services.Implementations;

public class PassengerServicesImpl(IPassengersRepository passengersRepository, IFlightRepository flightRepository) : IPassengerServices
{
    public List<Flight> SearchFlights(FlightFilter filter)
    {
        if (filter.IsEmpty())
        {
            throw new ArgumentException("Choose filters first");
        }
        return filter.All ?  flightRepository.GetFlightsByAvailability(true): flightRepository.SearchFlights(filter);
    }

    public Booking BookFlight(string flightId)
    {
        var flightToBook = flightRepository.GetFlightById(flightId);

        if (flightToBook == null)
        {
            throw new NotFoundException("Flight");
        }

        if (flightToBook.IsBooked)
        {
            throw new FlightAlreadyBookedException();
        }

        var currentUser = UserContext.CurrentUser as Passenger;
        var booking = new Booking { FlightId = flightId, PassengerName = currentUser!.Name };
        
        currentUser.AddBooking(booking);
        flightToBook.IsBooked = true;
        
        flightRepository.Update();
        passengersRepository.Update();
        
        return booking;
    }

    public void CancelBooking(string flightId)
    {
        var flightToCancel = flightRepository.GetFlightById(flightId);
        
        if (flightToCancel == null)
        {
            throw new NotFoundException("Flight");
        }
        
        var currentUser = UserContext.CurrentUser as Passenger;
        var bookingToCancel = currentUser!.Bookings.FirstOrDefault(b => b.FlightId == flightToCancel.Id);
        
        if (bookingToCancel == null)
        {
            throw new NotFoundException("Booking");
        }
        
        currentUser.RemoveBooking(bookingToCancel);
        flightToCancel.IsBooked = false;
        
        flightRepository.Update();
        passengersRepository.Update();
    }
}
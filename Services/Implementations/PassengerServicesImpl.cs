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

        var passenger = UserContext.CurrentUser as Passenger;
        var booking = new Booking { FlightId = flightId, PassengerName = passenger!.Name };
        
        passenger.AddBooking(booking);
        flightToBook.IsBooked = true;
        
        flightRepository.Update();
        passengersRepository.Update();
    }
}
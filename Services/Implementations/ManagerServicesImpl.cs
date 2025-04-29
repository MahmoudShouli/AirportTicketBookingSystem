using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.launchers;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Services.Implementations;

public class ManagerServicesImpl(IPassengersRepository passengersRepository, IFlightRepository flightRepository) : IManagerServices
{
    public List<string> ImportFlights(string filePath)
    {
        var flights = FileUtil.ConvertFileToList(filePath, CsvUtil.CsvToFlight);
        
        var report = FlightValidationUtil.ValidateAllFlights(flights);

        if (report.Count == 1)
        {
            FileUtil.SaveImportedFile(filePath, Program.Configuration["FilePaths:FlightsCsv"]!);
        }
        
        return report;
    }

    public List<Booking> FilterBookings(BookingFilter filter)
    {
        if (filter.IsEmpty())
        {
            throw new ArgumentException("Choose filters first");
        }
        
        if (!string.IsNullOrWhiteSpace(filter.PassengerName))
        {
            return passengersRepository.SearchPassengerByName(filter.PassengerName)!.Bookings;
        }
        
        var flights = flightRepository.SearchFlights(filter);
        var flightIDs = flights.Select(f=> f.Id).ToList();
        
        var allPassengers = passengersRepository.GetAllPassengers();
        var allBookings = allPassengers.SelectMany(p => p.Bookings).ToList();
        
        return allBookings.Where(b => flightIDs.Contains(b.FlightId)).ToList();
    }
}
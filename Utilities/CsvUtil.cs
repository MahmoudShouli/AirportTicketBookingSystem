using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilities;

public static class CsvUtil
{
    public static Passenger CsvToPassenger(string[] parts)
    {
        var name = parts.Length > 0 ? parts[0] : "";
        var password = parts.Length > 1 ? parts[1] : "";
        var bookingsRaw = parts.Length > 2 ? parts[2] : "";

        return new Passenger
        {
            Name = name,
            Password = password,
            Bookings = string.IsNullOrWhiteSpace(bookingsRaw)
                ? new List<Booking>()
                : bookingsRaw.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(flightId => new Booking { PassengerName = name, FlightId = flightId })
                    .ToList()
        };
    }


    public static Flight CsvToFlight(string[] parts)
    {
        return new Flight
        {
            Id = parts[0],
            Price = decimal.Parse(parts[1]),
            DepartureCountry = parts[2],
            DestinationCountry = parts[3],
            DepartureDate = DateTime.Parse(parts[4]),
            DepartureAirport = parts[5],
            DestinationAirport = parts[6],
            FlightClass = (FlightClass)Enum.Parse(typeof(FlightClass), parts[7], ignoreCase: true),
            IsBooked = bool.Parse(parts[8])
        };
    }

    public static List<string> PassengersToCsv(List<Passenger> passengers)
    {
        return new List<string> { "Name,Password,Bookings" }
            .Concat(passengers.Select(passenger =>
                $"{passenger.Name},{passenger.Password}," +
                $"{string.Join(';', passenger.Bookings.Select(b => b.FlightId.ToUpper()))}"
            ))
            .ToList();
    }

    public static List<string> FlightsToCsv(List<Flight> flights)
    {
        return new List<string> { "FlightId,Price,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,DestinationAirport,Class,IsBooked" }
            .Concat(flights.Select(flight => 
                $"{flight.Id},{flight.Price},{flight.DepartureCountry},{flight.DestinationCountry}," +
                $"{flight.DepartureDate:yyyy-MM-dd},{flight.DepartureAirport},{flight.DestinationAirport}," +
                $"{flight.FlightClass},{flight.IsBooked}"
            ))
            .ToList();
    }

}
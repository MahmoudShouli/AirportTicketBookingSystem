using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utils;

public static class Printer
{
    public static void PrintFlights(List<Flight> flights)
    {
        flights.ForEach(Console.WriteLine);
    }
}
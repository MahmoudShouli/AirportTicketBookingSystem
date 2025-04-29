using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Printers;

public static class HelperPrinter
{
    public static void PrintWelcomeMessage()
    {
        var message = """

                      Hello and Welcome to the Airport Ticket Booking System!

                      """;
        Console.WriteLine(message);
    }

    public static void PrintWelcomeSpecificUserMessage()
    {
        var currentUserName = UserContext.CurrentUser!.Name;
        var currentUserType = UserContext.GetCurrentUserType();
        Console.WriteLine($"Welcome {currentUserType.ToString()} : {currentUserName}");
    }
    public static void PrintFinishMessage()
    {
        var message = """

                      Thank you for using our system!

                      """;
        Console.WriteLine(message);
    }
    
    public static void PrintAnyKeyMessage(string prompt = "")
    {
        Console.WriteLine(prompt +"(Press any key to continue...)");
        Console.ReadLine();
    }
    
    public static void PrintFlights(List<Flight> flights)
    {
        if (flights.Count == 0)
            Console.WriteLine("No flights found.");
        else
        {
            Console.WriteLine();
            Console.WriteLine("Available Flights:");
            Console.WriteLine();
            flights.ForEach(Console.WriteLine);
            Console.WriteLine();
        }
    }
    
    public static void PrintBookings(List<Booking> bookings)
    {
        if (bookings.Count == 0)
            Console.WriteLine("No bookings found.");
        else
        {
            Console.WriteLine();
            Console.WriteLine("Bookings:");
            Console.WriteLine();
            bookings.ForEach(Console.WriteLine);
            Console.WriteLine();
        }
    }
}
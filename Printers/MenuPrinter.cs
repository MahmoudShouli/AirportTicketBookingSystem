namespace AirportTicketBookingSystem.Printers;

public static class MenuPrinter
{
    public static void PrintMainMenu()
    {
        var message = """

                      Registering Page
                      --------------------
                      
                      
                      1- Login
                      2- Register
                      3- Exit
                      

                      """;
        Console.WriteLine(message);
    }
    
    public static void PrintManagerMenu()
    {
        var message = """

                      Manager Dashboard
                      --------------------


                      1- Filter bookings
                      2- Import list of flights from a CSV file
                      3- Logout


                      """;
        Console.WriteLine(message);
    }

    public static void PrintFilterBookingsMenu()
    {
        var message = """

                      Booking Filters
                      --------------------

                      1. All bookings
                      2. Price
                      3. Departure Country
                      4. Destination Country
                      5. Departure Date
                      6. Departure Airport
                      7. Arrival Airport
                      8. Class
                      9. Passenger
                      10. Exit

                      """;

        Console.WriteLine(message);
    }

    public static void PrintPassengerMenu()
    {
        var message = """

                      Passenger Dashboard
                      --------------------

                      1. Search for a flight
                      2. Book a flight
                      3. View your bookings
                      4. Cancel a booking
                      5. Logout

                      """;

        Console.WriteLine(message);
    }

    public static void PrintSearchFlightsMenu()
    {
        var message = """

                      Flight Filters
                      --------------------

                      1. All flights
                      2. Price
                      3. Departure Country
                      4. Destination Country
                      5. Departure Date
                      6. Departure Airport
                      7. Destination Airport
                      8. Class
                      9. Exit

                      """;

        Console.WriteLine(message);
    }
    
    public static void PrintWelcomeMessage()
    {
        var message = """

                      Hello and Welcome to the Airport Ticket Booking System!

                      """;
        Console.WriteLine(message);
    }

    public static void PrintFinishMessage()
    {
        var message = """

                      Thank you for using our system!

                      """;
        Console.WriteLine(message);
    }

    public static void PrintInvalidMessage(string prompt)
    {
        Console.WriteLine("Invalid input, please " + prompt + "\n");
    }
}
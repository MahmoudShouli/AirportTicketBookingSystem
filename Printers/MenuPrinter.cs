using AirportTicketBookingSystem.Context;

namespace AirportTicketBookingSystem.Printers;

public static class MenuPrinter
{
    public static void PrintMainMenu()
    {
        Console.Clear();
        HelperPrinter.PrintWelcomeMessage();
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
        Console.Clear();
        HelperPrinter.PrintWelcomeSpecificUserMessage();
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
        Console.Clear();
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
        Console.Clear();
        HelperPrinter.PrintWelcomeSpecificUserMessage();
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
        Console.Clear();
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
    
    public static void PrintValidationDetails()
    {
        Console.Clear();
        var message = """
                      Validation Details
                      ----------------

                      * Flight ID
                      - Type       : string
                      - Constraints: required
                                     unique

                      * Price
                      - Type       : decimal
                      - Constraints: required
                      

                      * Departure Country
                      - Type       : string
                      - Constraints: required
                      

                      * Destination Country
                      - Type       : string
                      - Constraints: required, cannot be the same as Departure Country
                      

                      * Departure Date
                      - Type       : DateTime
                      - Constraints: required, must be in the future
                      

                      * Departure Airport
                      - Type       : string
                      - Constraints: required
                      

                      * Destination Airport
                      - Type       : string
                      - Constraints: required
                                     cannot be the same as Departure Airport

                      * Class
                      - Type       : enum (Economy, Business, FirstClass)
                      - Constraints: required, must match exactly
                      
                      """;

        Console.WriteLine(message);
    }
}
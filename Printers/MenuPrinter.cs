using Microsoft.Extensions.Logging;

namespace AirportTicketBookingSystem.Printers;

public class MenuPrinter
{
    private readonly ILogger<MenuPrinter> _logger;

    public MenuPrinter(ILogger<MenuPrinter> logger)
    {
        _logger = logger;
    }

    public void PrintWelcomeMessage()
    {
        var message = """
                      
                      Hello and Welcome to the Airport Ticket Booking System!
                      
                      """;
        _logger.LogInformation(message);
    }

    public void PrintFinishMessage()
    {
        var message = """

                      Thank you for using our system!

                      """;
        _logger.LogInformation(message);
    }

    public void PrintMainMenu()
    {
        var message = """

                      Registering Page
                      --------------------
                      
                      
                      What is your role?
                      1- Login
                      2- Sign up
                      3- Exit
                      

                      """;
        _logger.LogInformation(message);
    }
    
    public void PrintManagerMenu()
    {
        var message = """

                      Manager Dashboard
                      --------------------


                      1- Filter bookings
                      2- Import list of flights from a CSV file
                      3- Logout


                      """;
        _logger.LogInformation(message);
    }

    public void PrintFilterBookingsMenu()
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

        _logger.LogInformation(message);
    }

    public void PrintPassengerMenu()
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

        _logger.LogInformation(message);
    }

    public void PrintSearchFlightsMenu()
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
                      7. Arrival Airport
                      8. Class
                      9. Exit

                      """;

        _logger.LogInformation(message);
    }
}
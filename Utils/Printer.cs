using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Utils;

public class Printer(PassengerServices passengerServices, FlightService flightServices, ManagerServices managerServices)
{
    private Passenger _passenger;
    private static void PrintFlights(List<Flight> flights)
    {
        
        if (flights.Count == 0)
            Console.WriteLine("No flights found.");
        else
        {
            Console.WriteLine();
            Console.WriteLine("Available Flights:");
            Console.WriteLine();
            flights.ForEach(Console.WriteLine);
        }
        
    }
    
    private static void PrintBookings(List<Booking> bookings)
    {
        
        if (bookings.Count == 0)
            Console.WriteLine("No bookings found.");
        else
        {
            Console.WriteLine();
            Console.WriteLine("Bookings:");
            Console.WriteLine();
            bookings.ForEach(Console.WriteLine);
        }
        
    }
    
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Airport Ticket Booking System!");
            Console.WriteLine("What is your role?:");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");
            Console.WriteLine("Your choice: ");
            var userInput = Console.ReadLine();
            
            switch (userInput)
            {
                case "1":
                    ShowPassengerAuthOptions();
                    break;
                case "2":
                    ShowManagerMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
        
    }

    private void ShowPassengerAuthOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign up");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Your choice: ");
            var userInput = Console.ReadLine();
            
            switch (userInput)
            {
                case "1":
                    ShowPassengerLogin();
                    break;
                case "2":
                    ShowPassengerSignup();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }



    }

    private void ShowPassengerSignup()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your username or enter Q to exit.");
            var userInput = Console.ReadLine();

            if (userInput == "Q")
                return;
            
            _passenger = passengerServices.AuthenticatePassenger(userInput);

            if (_passenger == null)
            {
                _passenger = new Passenger { Name = userInput };
                passengerServices.RegisterPassenger(_passenger);
                ShowPassengerMenu();
            }
            else
            {
                Console.WriteLine("username is already taken. ");
                ShowAnyKeyMessage();
            }
        }
        

    }

    private void ShowPassengerLogin()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your username or enter Q to exit.");
            var userInput = Console.ReadLine();

            if (userInput == "Q")
                return;
            
            _passenger = passengerServices.AuthenticatePassenger(userInput);

            if (_passenger == null)
            {
                Console.WriteLine("Can't find you.");
                ShowAnyKeyMessage();
            }
            else
            {
                ShowPassengerMenu();
            }
        }
    }

    private void ShowManagerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome Manager!");
            Console.WriteLine("What would you like to do?:");
            Console.WriteLine("1. Filter bookings");
            Console.WriteLine("2. Import list of flights from a CSV file");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Your choice: ");
            var userInput = Console.ReadLine();
            
            switch (userInput)
            {
                case "1":
                    ShowFilterBookingsMenu();
                    break;
                case "2":
                    ShowImportMenu();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
    }

    private void ShowImportMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Before you import, these are important details to look out for in your file:");
            Console.WriteLine();
            ShowValidationDetails();
            Console.WriteLine();
            
            Console.WriteLine("Enter the path to the CSV file you want to import:");
            var filePath = Console.ReadLine();
            
            FileServices.SaveImportedFile(filePath);
            
        }
        
        
        
    }

    private static void ShowValidationDetails()
    {
        Console.WriteLine("Validation Details:\n");

        Console.WriteLine("* Flight ID");
        Console.WriteLine("  - Type       : string");
        Console.WriteLine("  - Constraints: required");
        Console.WriteLine("                 unique\n");
        
        Console.WriteLine("* Price");
        Console.WriteLine("  - Type       : decimal");
        Console.WriteLine("  - Constraints: required");

        Console.WriteLine("* Departure Country");
        Console.WriteLine("  - Type       : string");
        Console.WriteLine("  - Constraints: required\n");

        Console.WriteLine("* Destination Country");
        Console.WriteLine("  - Type       : string");
        Console.WriteLine("  - Constraints: required, cannot be the same as Departure Country\n");

        Console.WriteLine("* Departure Date");
        Console.WriteLine("  - Type       : DateTime");
        Console.WriteLine("  - Constraints: required, must be in the future\n");

        Console.WriteLine("* Departure Airport");
        Console.WriteLine("  - Type       : string");
        Console.WriteLine("  - Constraints: required\n");

        Console.WriteLine("* Destination Airport");
        Console.WriteLine("  - Type       : string");
        Console.WriteLine("  - Constraints: required");
        Console.WriteLine("                 cannot be the same as Departure Airport\n");

        Console.WriteLine("* Class");
        Console.WriteLine("  - Type       : enum (Economy, Business, FirstClass)");
        Console.WriteLine("  - Constraints: required, must match exactly\n");

        Console.WriteLine("* IsBooked");
        Console.WriteLine("  - Type       : bool");
        Console.WriteLine("  - Constraints: defaults to false on import\n");
    }


    private void ShowFilterBookingsMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("What would you like to search by?:");
            Console.WriteLine("1. All bookings");
            Console.WriteLine("2. Price");
            Console.WriteLine("3. Departure Country");
            Console.WriteLine("4. Destination Country");
            Console.WriteLine("5. Departure Date");
            Console.WriteLine("6. Departure Airport");
            Console.WriteLine("7. Arrival Airport");
            Console.WriteLine("8. Class");
            Console.WriteLine("9. Passenger");
            Console.WriteLine("10. Exit");
            Console.WriteLine("Your choice: ");
            var searchParam = Console.ReadLine();
            var searchVal = "";
            
            if (searchParam != "1" && searchParam != "10")
            {
                Console.WriteLine("Enter value: ");
                searchVal = Console.ReadLine();
            }
            
            switch (searchParam)
            {
                case "1":
                    PrintBookings(managerServices.FilterBookings(keyword:"all"));
                    break;
                case "2":
                    PrintBookings(managerServices.FilterBookings(price: Convert.ToDecimal(searchVal)));
                    break;
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                    PrintBookings(managerServices.FilterBookings(keyword:searchVal));
                    break;
                case "8":
                    if (Enum.TryParse<Class>(searchVal, ignoreCase: true, out var parsedClass))
                    {
                        PrintBookings(managerServices.FilterBookings(flightClass: parsedClass));
                    }
                    else
                    {
                        Console.WriteLine("Invalid class type. Please enter one of: Economy, Business, FirstClass.");
                    }
                    break;
                case "9":
                    PrintBookings(managerServices.FilterBookings(passenger:searchVal));
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break; 
            }
            ShowAnyKeyMessage();
        }
    }

    private void ShowPassengerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {_passenger.Name}!");
            Console.WriteLine("What would you like to do?:");
            Console.WriteLine("1. Search for a flight");
            Console.WriteLine("2. Book a flight");
            Console.WriteLine("3. View your bookings");
            Console.WriteLine("4. Cancel a booking");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Your choice: ");
            var userInput = Console.ReadLine();
            
            switch (userInput)
            {
                case "1":
                    ShowSearchFlightMenu();
                    break;
                case "2":
                    BookMenu();
                    break;
                case "3":
                    PrintBookings(_passenger.Bookings);
                    ShowAnyKeyMessage();
                    break;
                case "4":
                    ShowCancellation();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
    }

    private void ShowCancellation()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter the flight ID that you booked");
            var userInput = Console.ReadLine();
            
            var result = passengerServices.CancelBooking(_passenger, userInput);

            ShowAnyKeyMessage(result);
            return;
        }
    }

    private void BookMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter the flight ID");
            var userInput = Console.ReadLine();
            
            var booking = flightServices.BookFlight(_passenger, userInput);

            if (booking == null)
            {
                ShowAnyKeyMessage("This flight is already booked or it doesn't exist.");
            }
            else
            {
                ShowAnyKeyMessage("Flight booked!");
                return;
            }
        }
    }

    private void ShowSearchFlightMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("What would you like to search by?:");
            Console.WriteLine("1. All flights");
            Console.WriteLine("2. Price");
            Console.WriteLine("3. Departure Country");
            Console.WriteLine("4. Destination Country");
            Console.WriteLine("5. Departure Date");
            Console.WriteLine("6. Departure Airport");
            Console.WriteLine("7. Arrival Airport");
            Console.WriteLine("8. Class");
            Console.WriteLine("9. Exit");
            Console.WriteLine("Your choice: ");
            var searchParam = Console.ReadLine();
            var searchVal = "";
            
            if (searchParam != "1" && searchParam != "9")
            {
                Console.WriteLine("Enter value: ");
                searchVal = Console.ReadLine();
            }
            
            switch (searchParam)
            {
                case "1":
                    PrintFlights(flightServices.SearchFlights(false, keyword:"all"));
                    break;
                case "2":
                    PrintFlights(flightServices.SearchFlights(false, price: Convert.ToDecimal(searchVal)));
                    break;
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                    PrintFlights(flightServices.SearchFlights(false, keyword:searchVal));
                    break;
                case "8":
                    if (Enum.TryParse<Class>(searchVal, ignoreCase: true, out var parsedClass))
                    {
                        PrintFlights(flightServices.SearchFlights(false, flightClass: parsedClass));
                    }
                    else
                    {
                        Console.WriteLine("Invalid class type. Please enter one of: Economy, Business, FirstClass.");
                    }
                    break;
                case "9":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break; 
            }
            ShowAnyKeyMessage();
        }
    }
    
    private static void ShowAnyKeyMessage(string? message = null)
    {
        Console.WriteLine();
        Console.WriteLine(message + " Press any key to continue...");
        Console.ReadKey();
    }
}
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Utils;

public class Printer(PassengerServices passengerServices, FlightService flightServices, ManagerServices managerServices)
{
    private Passenger? _passenger;
    
    public void PrintMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Airport Ticket Booking System!");
            Console.WriteLine("What is your role?:");
            Console.WriteLine("1. Passenger");
            Console.WriteLine("2. Manager");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Your choice: ");
            
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    PrintPassengerAuthOptions();
                    break;
                case "2":
                    PrintManagerMenu();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
    }

    private void PrintPassengerAuthOptions()
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
                    PrintPassengerLogin();
                    break;
                case "2":
                    PrintPassengerSignup();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
    }

    private void PrintPassengerSignup()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your username or enter Q to exit.");
            
            var userInput = Console.ReadLine();
            if (userInput is null || userInput == "Q")
                return;
            
            _passenger = passengerServices.AuthenticatePassenger(userInput);

            if (_passenger == null)
            {
                _passenger = new Passenger { Name = userInput };
                passengerServices.RegisterPassenger(_passenger);
                PrintPassengerMenu();
            }
            else
            {
                Console.WriteLine("username is already taken. ");
                PrintAnyKeyMessage();
            }
        }
        

    }

    private void PrintPassengerLogin()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter your username or enter Q to exit.");
            var userInput = Console.ReadLine();

            if (userInput is null || userInput == "Q")
                return;
            
            _passenger = passengerServices.AuthenticatePassenger(userInput);

            if (_passenger == null)
            {
                Console.WriteLine("Can't find you.");
                PrintAnyKeyMessage();
            }
            else
            {
                PrintPassengerMenu();
            }
        }
    }

    private void PrintManagerMenu()
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
                    PrintFilterBookingsMenu();
                    break;
                case "2":
                    PrintImportMenu();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
    }

    private static void PrintImportMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nBefore you import, these are important details to look out for in your file:");
            Console.WriteLine();
            PrintValidationDetails();
            Console.WriteLine();
        
            Console.WriteLine("Enter the path to the CSV file you want to import or Q to exit:");
            var userInput = Console.ReadLine();
            
            if (userInput is null || userInput == "Q")
                return;

            var flights = FileServices.ConvertFileToFlights(userInput);
            if (flights.Count == 0)
            {
                PrintAnyKeyMessage("There are no flights to import.");
                continue;
            }
            
            var isValid = FileServices.ValidateAllFlights(flights);
            if (!isValid)
            {
                PrintAnyKeyMessage("Check again and come back.");
                
            }
            else
            {
                Console.WriteLine("All validations passed!");
                FileServices.SaveImportedFile(userInput);
                PrintAnyKeyMessage();
                
            }

            return;

        }
        
    }

    private static void PrintValidationDetails()
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
    }


    private void PrintFilterBookingsMenu()
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
            PrintAnyKeyMessage();
        }
    }

    private void PrintPassengerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {_passenger!.Name}!");
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
                    PrintSearchFlightMenu();
                    break;
                case "2":
                    PrintBookMenu();
                    break;
                case "3":
                    PrintBookings(_passenger!.Bookings);
                    PrintAnyKeyMessage();
                    break;
                case "4":
                    PrintCancellation();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
            }
        }
    }

    private void PrintCancellation()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter the flight ID that you booked");
            var userInput = Console.ReadLine();
            
            var result = passengerServices.CancelBooking(_passenger!, userInput!);

            PrintAnyKeyMessage(result);
            return;
        }
    }

    private void PrintBookMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter the flight ID");
            var userInput = Console.ReadLine();
            
            var booking = flightServices.BookFlight(_passenger!, userInput!);

            if (booking == null)
            {
                PrintAnyKeyMessage("This flight is already booked or it doesn't exist.");
                break;
            }
            else
            {
                PrintAnyKeyMessage("Flight booked!");
                return;
            }
        }
    }

    private void PrintSearchFlightMenu()
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
            PrintAnyKeyMessage();
        }
    }
    
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
    
    private static void PrintAnyKeyMessage(string? message = null)
    {
        Console.WriteLine();
        Console.WriteLine(message + " Press any key to continue...");
        Console.ReadKey();
    }
}
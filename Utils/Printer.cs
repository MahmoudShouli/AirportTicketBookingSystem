using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Utils;

public class Printer(PassengerServices passengerServices)
{
    private Passenger _passenger;
    private static void PrintFlights(List<Flight> flights)
    {
        if (flights.Count == 0)
            Console.WriteLine("No flights found.");
        
        flights.ForEach(Console.WriteLine);
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
                passengerServices.RegisterPassenger(_passenger);
                ShowPassengerMenu();
            }
            else
            {
                Console.WriteLine("username is already taken.");
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
        throw new NotImplementedException();
    }

    private void ShowPassengerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome Passenger!");
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
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            
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
                    PrintFlights(passengerServices.SearchFlightsForPassengers(keyword:"all"));
                    break;
                case "2":
                    PrintFlights(passengerServices.SearchFlightsForPassengers(price: Convert.ToDecimal(searchVal)));
                    break;
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                    PrintFlights(passengerServices.SearchFlightsForPassengers(keyword:searchVal));
                    break;
                case "8":
                    if (Enum.TryParse<Class>(searchVal, ignoreCase: true, out var parsedClass))
                    {
                        PrintFlights(passengerServices.SearchFlightsForPassengers(flightClass: parsedClass));
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
        Console.WriteLine(message + "Press any key to continue...");
        Console.ReadKey();
    }
}
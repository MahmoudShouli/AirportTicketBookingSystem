using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Controllers;

// controller for authentication
public class AuthController(IAuthService authService)
{
    public bool Login(string name, string password) 
    {
        try
        {
            authService.Login(name, password);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
            return false;
        }
    }

    public bool Register(string name, string password)
    {
        try
        {
            authService.Register(name, password);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration failed: {ex.Message}");
            return false;
        }
    }
}
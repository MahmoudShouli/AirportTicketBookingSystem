using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Controllers;

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
}
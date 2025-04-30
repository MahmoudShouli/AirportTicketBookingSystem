namespace AirportTicketBookingSystem.Services;

public interface IAuthService
{
    void Login(string name, string password);
    void Register(string name, string password);
}
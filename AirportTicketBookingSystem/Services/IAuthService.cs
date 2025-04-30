using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public interface IAuthService
{
    User Login(string name, string password);
    void Register(string name, string password);
}
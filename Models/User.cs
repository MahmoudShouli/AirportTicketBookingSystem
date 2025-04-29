using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class User
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType UserType { get; set; }
}
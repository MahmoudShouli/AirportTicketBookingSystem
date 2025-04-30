using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.Models;

public class Manager : User
{
    public Manager(string name, string password)
    {
        Name = name;
        Password = password;
        UserType = UserType.Manager;
    }
}
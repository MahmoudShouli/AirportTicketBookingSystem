using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Context;

public static class UserContext
{
    public static User? CurrentUser { get; private set; }
    
    public static void SetCurrentUser(User user)
    {
        CurrentUser = user;
    }

    public static void ResetCurrentUser()
    {
        CurrentUser = null;
    }

    public static UserType GetCurrentUserType()
    {
        return CurrentUser!.UserType;
    }
}
using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.launchers;
using AirportTicketBookingSystem.Printers;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Views;

public static class AuthView
{
    public static void LoginHandler()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Login ====\n");
            
            var name = ScannerUtil.ScanNonEmptyString("your name: ");
            var password = ScannerUtil.ScanNonEmptyString("your password: ");
            
            bool success = AppStartup.AuthController.Login(name, password);

            if (success)
            {
                var userType = UserContext.GetCurrentUserType();
                if (userType == UserType.Manager)
                {
                    MenuHandler.ManagerMenuHandler();
                    return;
                }
                else if (userType == UserType.Passenger)
                {
                    MenuHandler.PassengerMenuHandler();
                    return;
                }
            }
            HelperPrinter.PrintAnyKeyMessage();
            return;
        }
    }

    public static void RegisterHandler()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Registration ====\n");
            
            var name = ScannerUtil.ScanNonEmptyString("your name: ");
            var password = ScannerUtil.ScanNonEmptyString("your password: ");
            
            bool success = AppStartup.AuthController.Register(name, password);

            if (success)
            {
                MenuHandler.PassengerMenuHandler();
            }
            HelperPrinter.PrintAnyKeyMessage();
            return;
        }
    }
    
    public static void LogoutHandler()
    {
        UserContext.ResetCurrentUser();
    }
}
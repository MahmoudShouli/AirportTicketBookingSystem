using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Printers;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.launchers;

public static class MenuHandler
{
    public static void MainMenuHandler()
    {
        MenuPrinter.PrintWelcomeMessage();
        MenuPrinter.PrintMainMenu();
        
        while (true)
        {
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                case 1:
                    LoginHandler();
                    break;
                case 2:
                    RegisterHandler();
                    break;
                case 3:
                    MenuPrinter.PrintFinishMessage();
                    return;
                default:
                    MenuPrinter.PrintInvalidMessage("enter a valid choice");
                    break;
            }
        }
    }
    
    private static void LoginHandler()
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
                    ManagerMenuHandler();
                }
                else if (userType == UserType.Passenger)
                {
                    PassengerMenuHandler();
                }
            }
            Thread.Sleep(4000);
        }
    }

    private static void RegisterHandler()
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
                PassengerMenuHandler();
            }
            Thread.Sleep(4000);
        }
    }
    
    private static void ManagerMenuHandler()
    {
        
    }

    private static void PassengerMenuHandler()
    {
        
    }
}
using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Printers;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.launchers;

public static class MenuHandler
{
    public static void MainMenuHandler()
    {
        while (true)
        {
            MenuPrinter.PrintMainMenu();
            
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
                    HelperPrinter.PrintFinishMessage();
                    return;
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 3)");
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
                    return;
                }
                else if (userType == UserType.Passenger)
                {
                    PassengerMenuHandler();
                    return;
                }
            }
            HelperPrinter.PrintAnyKeyMessage();
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
            HelperPrinter.PrintAnyKeyMessage();
        }
    }
    
    private static void ManagerMenuHandler()
    {
        while (true)
        {
            MenuPrinter.PrintManagerMenu();
            
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                case 1:
                    MenuPrinter.PrintFilterBookingsMenu();
                    break;
                case 2:
                    ImportHandler();
                    break;
                case 3:
                    LogoutHandler();
                    return;
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 3)");
                    break;
            }
        }
    }
    
    private static void ImportHandler()
    {
        while (true)
        {
            MenuPrinter.PrintValidationDetails();
            
            var input = ScannerUtil.ScanNonEmptyString("your file path or Q to exit: ");
            
            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                return;
            
            var report = AppStartup.ManagerController.ImportFlights(input);

            // 1 means the report only contains the header (no errors added to it)
            if (report.Count == 1)
            {
                HelperPrinter.PrintAnyKeyMessage("Import success!");
                return;
            }
            
            report.ForEach(Console.WriteLine);
            HelperPrinter.PrintAnyKeyMessage();
        }
    }

    private static void PassengerMenuHandler()
    {
        
    }
    
    private static void LogoutHandler()
    {
        UserContext.ResetCurrentUser();
    }
}
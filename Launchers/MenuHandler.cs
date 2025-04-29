using AirportTicketBookingSystem.Context;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Printers;
using AirportTicketBookingSystem.Utilities;
using AirportTicketBookingSystem.Views;

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
                    AuthView.LoginHandler();
                    break;
                
                case 2:
                    AuthView.RegisterHandler();
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
    
    public static void ManagerMenuHandler()
    {
        while (true)
        {
            MenuPrinter.PrintManagerMenu();
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                case 1:
                    ManagerView.FilterBookingsHandler();
                    break;
                
                case 2:
                    ManagerView.ImportHandler();
                    break;
                
                case 3:
                    AuthView.LogoutHandler();
                    return;
                
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 3)");
                    break;
            }
        }
    }
    
    public static void PassengerMenuHandler()
    {
        while (true)
        {
            MenuPrinter.PrintPassengerMenu();
            var choice = ScannerUtil.ScanInt("choice");

            switch (choice)
            {
                case 1:
                    PassengerView.SearchHandler();
                    break;
                
                case 2:
                    PassengerView.BookHandler();
                    break;
                
                case 3:
                    PassengerView.PrintBookingHandler();
                    break;
                
                case 4:
                    PassengerView.CancellationHandler();
                    break;
                
                case 5:
                    AuthView.LogoutHandler();
                    return;
                
                default:
                    HelperPrinter.PrintAnyKeyMessage("Invalid choice (must be 1 - 10)");
                    break;
            }
        }
    }
}
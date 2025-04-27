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
                    SignUpHandler();
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
        throw new NotImplementedException();
    }
    
    private static void SignUpHandler()
    {
        throw new NotImplementedException();
    }
}
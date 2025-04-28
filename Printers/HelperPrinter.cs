using AirportTicketBookingSystem.Context;

namespace AirportTicketBookingSystem.Printers;

public static class HelperPrinter
{
    public static void PrintWelcomeMessage()
    {
        var message = """

                      Hello and Welcome to the Airport Ticket Booking System!

                      """;
        Console.WriteLine(message);
    }

    public static void PrintWelcomeSpecificUserMessage()
    {
        var currentUserName = UserContext.CurrentUser!.Name;
        var currentUserType = UserContext.GetCurrentUserType();
        Console.WriteLine($"Welcome {currentUserType.ToString()} : {currentUserName}");
    }
    public static void PrintFinishMessage()
    {
        var message = """

                      Thank you for using our system!

                      """;
        Console.WriteLine(message);
    }
    
    public static void PrintAnyKeyMessage(string prompt = "")
    {
        Console.WriteLine(prompt +"(Press any key to continue...)");
        Console.ReadLine();
    }
}
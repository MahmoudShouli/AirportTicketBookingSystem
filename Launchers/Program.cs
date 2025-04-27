using AirportTicketBookingSystem.Printers;
using Microsoft.Extensions.Configuration;

namespace AirportTicketBookingSystem.launchers;

class Program
{
    public static IConfiguration Configuration = null!;
    static void Main()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        Configuration = new ConfigurationBuilder()
            .SetBasePath(projectRoot) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        MenuHandler.MainMenuHandler();
    }
}
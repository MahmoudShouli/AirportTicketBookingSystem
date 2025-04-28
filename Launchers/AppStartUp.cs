using AirportTicketBookingSystem.Controllers;
using AirportTicketBookingSystem.Repositories;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Services.Implementations;

namespace AirportTicketBookingSystem.launchers;

#nullable disable
public static class AppStartup
{
    public static IAuthService AuthService { get; private set; }
    public static IManagerServices ManagerServices { get; private set; }
    public static AuthController AuthController { get; private set; }
    public static ManagerController ManagerController { get; private set; }
    
    public static void Init()
    {
        var passengerRepo = new PassengersFileRepository(Program.Configuration["FilePaths:PassengersCsv"]!);

        AuthService = new AuthServiceImpl(passengerRepo);
        ManagerServices = new ManagerServicesImpl();
        
        AuthController = new AuthController(AuthService);
        ManagerController = new ManagerController(ManagerServices);
    }
}

using AirportTicketBookingSystem.Controllers;
using AirportTicketBookingSystem.Repositories;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Services.Implementations;

namespace AirportTicketBookingSystem.launchers;

#nullable disable
public static class AppStartup
{
    private static IAuthService AuthService { get; set; }
    private static IManagerServices ManagerServices { get; set; }
    private static IPassengerServices PassengerServices { get; set; }
    
    public static AuthController AuthController { get; private set; }
    public static ManagerController ManagerController { get; private set; }
    public static PassengerController PassengerController { get; private set; }
    
    public static void Init()
    {
        var passengerRepo = new PassengersFileRepository(Program.Configuration["FilePaths:PassengersCsv"]!);
        var flightRepo = new FlightFileRepository(Program.Configuration["FilePaths:FlightsCsv"]!);
        
        AuthService = new AuthServiceImpl(passengerRepo);
        ManagerServices = new ManagerServicesImpl(passengerRepo, flightRepo);
        PassengerServices = new PassengerServicesImpl(passengerRepo, flightRepo);
        
        AuthController = new AuthController(AuthService);
        ManagerController = new ManagerController(ManagerServices);
        PassengerController = new PassengerController(PassengerServices);
    }
}

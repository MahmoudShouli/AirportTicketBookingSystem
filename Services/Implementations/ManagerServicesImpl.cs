using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.launchers;
using AirportTicketBookingSystem.Repositories;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Services.Implementations;

public class ManagerServicesImpl : IManagerServices
{
    public List<string> ImportFlights(string filePath)
    {
        var flights = FileUtil.ConvertFileToList(filePath, CsvUtil.CsvToFlight);
        
        var report = FlightValidationUtil.ValidateAllFlights(flights);

        if (report.Count == 1)
        {
            FileUtil.SaveImportedFile(filePath, Program.Configuration["FilePaths:FlightsCsv"]!);
        }
        
        return report;
    }
}
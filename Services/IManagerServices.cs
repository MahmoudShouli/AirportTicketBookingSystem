namespace AirportTicketBookingSystem.Services;

public interface IManagerServices
{
    List<string> ImportFlights(string filePath);
}
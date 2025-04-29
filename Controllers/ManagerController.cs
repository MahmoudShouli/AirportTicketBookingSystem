using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Controllers;

public class ManagerController(IManagerServices managerService)
{
    public List<string> ImportFlights(string filePath)
    {
        var report  = new List<string>();
        try
        {
            report = managerService.ImportFlights(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Import Failed: {e.Message}");
        }
       
        return report;
    }
    
    public List<Booking> FilterBookings(BookingFilter filter)
    {
        var bookings = new List<Booking>();
        try
        {
            bookings = managerService.FilterBookings(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return bookings;
    }
}
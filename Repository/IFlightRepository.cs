namespace AirportTicketBookingSystem.Repository;

public interface IFlightRepository<T>
{
     List<T> LoadData();
}
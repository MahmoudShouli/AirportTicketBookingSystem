namespace AirportTicketBookingSystem.Data;

public interface IDataStore<T>
{
    List<T> LoadData();
}
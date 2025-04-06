using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Repository;

public class PassengersRepository : IPassengersRepository
{
        private readonly List<Passenger> _passengers = [
                new Passenger { Name = "Mahmoud" },
                new Passenger { Name = "Fatima" }
        ];
        
        public void AddPassenger(Passenger p)
        {
                _passengers.Add(p);
        }

        public List<Passenger> GetAllPassengers() => _passengers;

        public Passenger? GetPassengerByName(string name)
        {
                return _passengers.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
}

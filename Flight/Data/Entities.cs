using System;
using Flight.Domain.Entities;

namespace Flight.Data
{
    public class Entities
    {
        public IList<Passenger> Passengers = new List<Passenger>();

        public List<FlightInfo> Flights = new List<FlightInfo>();
    }
}

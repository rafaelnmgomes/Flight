using Flight.Domain.Errors;
using Flight.ReadModels;

namespace Flight.Domain.Entities
{
    public class FlightInfo
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Booking> Bookings = new List<Booking>();

        public FlightInfo()
        {

        }

        public FlightInfo(
            Guid id,
            string airline,
            string price,
            TimePlace departure,
            TimePlace arrival,
            int remainingNumberOfSeats
        )
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;
        }

        public object? MakeBooking(string passengerEmail, byte numberOfSeats)
        {
            var flight = this;

            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }

            flight.Bookings.Add(new Booking(passengerEmail, numberOfSeats));

            flight.RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }

        public object? CancelBooking(string passengerEmail, byte numberOfSeats)
        {
            var booking = Bookings.FirstOrDefault(b => numberOfSeats == b.NumberOfSeats && passengerEmail.ToLower() == b.PassengerEmail.ToLower());

            if(booking == null)
            {
                return new NotFound();
            }

            Bookings.Remove(booking);
            RemainingNumberOfSeats += booking.NumberOfSeats;

            return null;
        }
    }
}
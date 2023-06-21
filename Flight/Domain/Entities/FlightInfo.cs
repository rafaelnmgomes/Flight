namespace Flight.Domain.Entities
{
    public record FlightInfo(
        Guid Id,
        string Airline,
        string Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        )
    {
        public IList<Booking> Bookings = new List<Booking>();
    }
}
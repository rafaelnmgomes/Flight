namespace Flight.Domain.Entities
{
    public record Booking(
        Guid FlightId,
        string PassengerEmail,
        byte NumberOfSeats);

}

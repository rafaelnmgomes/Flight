﻿using Flight.Domain.Errors;
using Flight.ReadModels;

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
        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

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
    }
}
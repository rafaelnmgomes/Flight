using Flight.Data;
using Flight.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Flight.Dtos;
using Flight.Domain.Errors;

namespace Flight.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities)
        {
            _entities = entities;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
        public ActionResult<IEnumerable<BookingRm>> List(string email)
        {
            var bookings = _entities.Flights.ToArray()
                .SelectMany(f => f.Bookings.
                Where(b => b.PassengerEmail == email)
                .Select(b => new BookingRm(
                    f.Id,
                    f.Airline,
                    f.Price.ToString(),
                    new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                    new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                    b.NumberOfSeats,
                    email
                    )));

            return Ok(bookings);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Cancel(BookDto dto)
        {
            var flight = _entities.Flights.Find(dto.FlightId);

            var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if(error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }

            if (error is NotFound) { 
                return NotFound();
            }

            throw new Exception($"The error of type: {error.GetType().Name} occured while cancelling the booking made by {dto.PassengerEmail}");
        }
    }
}

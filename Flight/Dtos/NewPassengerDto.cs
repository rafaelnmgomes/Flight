using System.ComponentModel.DataAnnotations;

namespace Flight.Dtos
{
    public record NewPassengerDto(
        [Required][EmailAddress][StringLength(100, MinimumLength = 3)] string Email,
        [Required][MinLength(2)][MaxLength(50)] string FirstName,
        [Required][MinLength(2)][MaxLength(50)] string LastName,
        [Required] bool Gender
    );
}

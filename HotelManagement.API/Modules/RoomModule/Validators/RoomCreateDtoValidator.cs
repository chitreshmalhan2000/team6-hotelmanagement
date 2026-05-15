using FluentValidation;
using HotelManagement.API.Modules.RoomModule.DTOs;
namespace HotelManagement.API.Modules.RoomModule.Validators;

public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
{
    public RoomCreateDtoValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Room details are required.");

        RuleFor(x => x.RoomNumber)
            .GreaterThan(0)
            .WithMessage("Room number must be greater than 0.")
            .LessThanOrEqualTo(9999)
            .WithMessage("Room number cannot exceed 9999.");

        RuleFor(x => x.RoomTypeId)
            .GreaterThan(0)
            .WithMessage("A valid room type ID is required.");
    }

    public static void ValidateDto(RoomCreateDto dto)
    {
        new RoomCreateDtoValidator().ValidateAndThrow(dto);
    }
}

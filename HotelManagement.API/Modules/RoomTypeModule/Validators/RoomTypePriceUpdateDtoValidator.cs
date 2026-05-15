using FluentValidation;
using HotelManagement.API.Modules.RoomTypeModule.DTOs;

namespace HotelManagement.API.Modules.RoomTypeModule.Validators;
    
public class RoomTypePriceUpdateDtoValidator : AbstractValidator<RoomTypePriceUpdateDto>
{
    public RoomTypePriceUpdateDtoValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Room type price details are required.");

        RuleFor(x => x.PricePerNight)
            .InclusiveBetween(0.01m, 100000m)
            .WithMessage("Price per night must be between 0.01 and 100000.");
    }

    public static void ValidateDto(RoomTypePriceUpdateDto dto)
    {
        new RoomTypePriceUpdateDtoValidator().ValidateAndThrow(dto);
    }
}

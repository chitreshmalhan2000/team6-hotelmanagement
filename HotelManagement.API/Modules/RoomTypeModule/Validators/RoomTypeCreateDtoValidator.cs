using FluentValidation;
using HotelManagement.API.Modules.RoomTypeModule.DTOs;

namespace HotelManagement.API.Modules.RoomTypeModule.Validators;

public class RoomTypeCreateDtoValidator : AbstractValidator<RoomTypeCreateDto>
{
    public RoomTypeCreateDtoValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Room type details are required.");

        RuleFor(x => x.TypeName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Room type name is required.")
            .MinimumLength(2)
            .WithMessage("Room type name must be at least 2 characters.")
            .MaximumLength(255)
            .WithMessage("Room type name cannot exceed 255 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .WithMessage("Description cannot exceed 2000 characters.");

        RuleFor(x => x.MaxOccupancy)
            .InclusiveBetween(1, 20)
            .When(x => x.MaxOccupancy.HasValue)
            .WithMessage("Max occupancy must be between 1 and 20.");

        RuleFor(x => x.PricePerNight)
            .InclusiveBetween(0.01m, 100000m)
            .When(x => x.PricePerNight.HasValue)
            .WithMessage("Price per night must be between 0.01 and 100000.");
    }

    public static void ValidateDto(RoomTypeCreateDto dto)
    {
        Normalize(dto);
        new RoomTypeCreateDtoValidator().ValidateAndThrow(dto);
    }

    private static void Normalize(RoomTypeCreateDto dto)
    {
        if (dto is null) return;

        dto.TypeName = dto.TypeName?.Trim() ?? string.Empty;
        dto.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
    }
}

using FluentValidation;
using HotelManagement.API.Modules.ReservationModule.DTOs;
using HotelManagement.API.Modules.ReservationModule.Services;

namespace HotelManagement.API.Modules.ReservationModule.Validators;

public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationValidator(IReservationService reservationService)
    {
        // Guest Name
        RuleFor(x => x.GuestName)
            .NotEmpty()
            .WithMessage("Guest name is required.")
            .MinimumLength(2)
            .WithMessage("Guest name must be at least 2 characters.")
            .MaximumLength(100)
            .WithMessage("Guest name must not exceed 100 characters.")
            .Matches(@"^[a-zA-Z\s\.\-']+$")
            .WithMessage("Name contains invalid characters.");
        
        // Guest Email
        RuleFor(x => x.GuestEmail)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email address.")
            .MaximumLength(256)
            .WithMessage("Email must not exceed 256 characters.");
        
        // Guest Phone Number
        RuleFor(x => x.GuestPhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^(?:\+91|0)?[6-9]\d{9}$")
            .WithMessage("Invalid phone number format.");
        
        // Check-in Date
        RuleFor(x => x.CheckInDate)
            .NotEmpty()
            .Must(BeTodayOrFuture)
            .WithMessage("Check-in dates must be in the future");
        
        // Check-out Date
        RuleFor(x => x.CheckOutDate)
            .NotEmpty()
            .GreaterThan(x => x.CheckInDate)
            .WithMessage("Check-out dates must be after Check-in date.");
        
        // Room ID
        RuleFor(x => x.RoomId)
            .GreaterThan(0)
            .WithMessage("Invalid room id");

        RuleFor(x => x)
            .Must(HaveValidStayLength)
            .WithMessage("Stay length must be greater than or equal to 1.")
            .MustAsync(async (request, cancellationToken) =>
                !await reservationService.HasReservationDateConflictAsync(
                    request.RoomId,
                    request.CheckInDate,
                    request.CheckOutDate))
            .WithMessage("The room is already booked for selected dates.");
    }

    private static bool BeTodayOrFuture(DateOnly checkIn) =>
        checkIn == DateOnly.FromDateTime(DateTime.UtcNow);

    private static bool HaveValidStayLength(CreateReservationDto dto) =>
        (dto.CheckOutDate.DayNumber - dto.CheckInDate.DayNumber) >= 1;
}

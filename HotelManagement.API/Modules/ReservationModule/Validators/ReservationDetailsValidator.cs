using FluentValidation;
using HotelManagement.API.Modules.ReservationModule.DTOs;

namespace HotelManagement.API.Modules.ReservationModule.Validators;

public class ReservationDetailsValidator : AbstractValidator<ReservationDetailsDto>
{
    public ReservationDetailsValidator()
    {
        // Reservation ID
        RuleFor(x => x.ReservationId)
            .GreaterThan(0)
            .WithMessage("Reservation ID must be greater than zero.");
        
        // Guest Name
        RuleFor(x => x.GuestName)
            .NotEmpty()
            .WithMessage("Guest name is required.")
            .MinimumLength(2)
            .WithMessage("Guest name must be at least 2 characters long.")
            .MaximumLength(100)
            .WithMessage("Guest name must not exceed 100 characters.")
            .Matches(@"^[a-zA-Z\s\.\-']+$")
            .WithMessage("Guest name contains invalid characters.");
        
        // Guest Email
        RuleFor(x => x.GuestEmail)
            .NotEmpty()
            .WithMessage("Guest email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(256)
            .WithMessage("Guest email must not exceed 256 characters.");
        
        // Guest Phone Number
        RuleFor(x => x.GuestPhoneNumber)
            .NotEmpty()
            .WithMessage("Guest phone number is required.")
            .Matches(@"^(?:\+91|0)?[6-9]\d{9}$")
            .WithMessage("Invalid phone number.");
        
        // Check-in Date
        RuleFor(x => x.CheckInDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Check-in date cannot be in the past.");
        
        // Check-out Date
        RuleFor(x => x.CheckOutDate)
            .GreaterThan(x => x.CheckOutDate)
            .WithMessage("Check-out date must be after check-in date.");
        
        // Stay Duration
        RuleFor(x => x)
            .Must(HaveValidStayLength)
            .WithMessage("Stay must be for at least 1 night.");
        
        // Booking Date
        RuleFor(x => x.BookingDate)
            .NotEmpty()
            .WithMessage("Booking date is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Booking date cannot be in future.");
        
        // Room
        RuleFor(x => x.RoomNumber)
            .GreaterThan(0)
            .WithMessage("Invalid room number.");
        
        RuleFor(x => x.RoomTypeId)
            .GreaterThan(0)
            .WithMessage("Invalid room type id.");
        
        RuleFor(x => x.RoomType)
            .NotEmpty()
            .WithMessage("Room type is required.");
        
        // Price Per Night
        RuleFor(x => x.PricePerNight)
            .GreaterThan(0)
            .WithMessage("Invalid price.");
        
        // Amenities
        RuleFor(x => x.Amenities)
            .NotNull()
            .WithMessage("Amenities list should not be null.");
        
        // Hotel
        RuleFor(x => x.HotelId)
            .GreaterThan(0)
            .WithMessage("Invalid hotel id.");
        
        RuleFor(x => x.HotelName)
            .NotEmpty()
            .WithMessage("Hotel name is required.")
            .MinimumLength(3)
            .WithMessage("Hotel name should be  at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Hotel name must not exceed 100 characters.");
        
        RuleFor(x => x.HotelLocation)
            .NotEmpty()
            .WithMessage("Hotel location is required.")
            .MaximumLength(200)
            .WithMessage("Hotel location must not exceed 200 characters.");
        
        // Total Price
        RuleFor(x => x.TotalPrice)
            .GreaterThan(0)
            .WithMessage("Total price must be greater than zero.");
        
        RuleFor(x => x)
            .Must(HaveValidTotalPrice)
            .WithMessage("Total price does not match stay duration and nightly rate.");
    }
    
    private static bool HaveValidStayLength(ReservationDetailsDto dto) =>
        (dto.CheckOutDate.DayNumber - dto.CheckInDate.DayNumber) >= 1;

    private static bool HaveValidTotalPrice(ReservationDetailsDto dto)
    {
        var nights =
            dto.CheckOutDate.DayNumber -
            dto.CheckInDate.DayNumber;
        
        var expectedTotal = nights * dto.TotalPrice;
        return dto.TotalPrice >= expectedTotal;
    }
}
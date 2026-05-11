using HotelManagement.API.Modules.PaymentModule.DTOs;

namespace HotelManagement.API.Modules.PaymentModule.Services;

public interface IPaymentService
{
    Task<PaymentDetailsDto>  GetPaymentDetailsByReservationIdAsync(int reservationId);
}
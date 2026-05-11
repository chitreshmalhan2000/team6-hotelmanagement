using HotelManagement.API.Exceptions;
using HotelManagement.API.Modules.PaymentModule.DTOs;
using HotelManagement.API.Modules.PaymentModule.Repositories;

namespace HotelManagement.API.Modules.PaymentModule.Services;

public class PaymentService(IPaymentRepository paymentRepository) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    
    public async Task<PaymentDetailsDto> GetPaymentDetailsByReservationIdAsync(int reservationId)
    {
        var payment = await _paymentRepository.GetPaymentDetailsByIdAsync(reservationId) ??
                      throw new NotFoundException("Payment not found.");

        return new PaymentDetailsDto
        {
            ReservationId = reservationId,
            Amount = payment.Amount ?? 0,
            PaymentDate = payment.PaymentDate ?? DateOnly.MinValue,
            PaymentStatus = payment.PaymentStatus ??  string.Empty,
        };
    }
}
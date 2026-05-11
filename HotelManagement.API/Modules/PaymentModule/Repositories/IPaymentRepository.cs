using HotelManagement.API.Modules.PaymentModule.DTOs;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.PaymentModule.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentDetailsByIdAsync(int paymentId);
    Task<Payment?> GetPaymentDetailsByReservationIdAsync(int reservationId);
}
using HotelManagement.API.Modules.PaymentModule.DTOs;
using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.PaymentModule.Repositories;

public class PaymentRepository(HotelDbContext dbContext) : IPaymentRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<Payment?> GetPaymentDetailsByIdAsync(int paymentId) =>
        await _dbContext.Payments.FindAsync(paymentId);

    public async Task<Payment?> GetPaymentDetailsByReservationIdAsync(int reservationId) =>
        await _dbContext.Payments.FirstOrDefaultAsync(payment => payment.ReservationId == reservationId);
}
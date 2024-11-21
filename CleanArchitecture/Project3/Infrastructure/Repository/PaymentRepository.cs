using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class PaymentRepository : GenericRepository<Payment, int>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context, ILogger<PaymentRepository> logger) : base(context, logger)
        {
        }
        public override async Task<Payment?> GetById(int id)
        {
            try
            {
                return await _context.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.PaymentId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public async Task<List<Payment>> GetPaymentsByYear(int year)
        {
            try
            {
                return await _context.Payments
                    .AsNoTracking()
                    .Where(p => p.PaymentDate.Year == year)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while getting payments by year: {e}");
                return new List<Payment>();
            }
        }

    }
}

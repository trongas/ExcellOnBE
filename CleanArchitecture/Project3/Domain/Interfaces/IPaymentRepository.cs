using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment, int>
    {
        Task<List<Payment>> GetPaymentsByYear(int year);
    }
}

using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReportRepository : IGenericRepository<Report, int>
    {
    }
}

using Domain.Entities;
using Domain.Abstraction;

namespace Domain.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service, int>
    {
        Task<Service?> GetByName(string name);
    }
}

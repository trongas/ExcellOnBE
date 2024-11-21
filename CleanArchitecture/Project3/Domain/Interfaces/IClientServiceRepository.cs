using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClientServiceRepository : IGenericRepository<ClientService, int>
    {
        Task<int> GetTotalClientsByServiceId(int serviceId);
    }
}

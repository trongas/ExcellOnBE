using Domain.Entities;
using Domain.Abstraction;
namespace Domain.Interfaces
{
    public interface IServiceChargesRepository : IGenericRepository<ServiceCharges,int>
    {
        Task<ServiceCharges?> GetByName(string serviceName);
        Task<List<ServiceCharges>> GetByServiceId(int serviceId);

    }
}

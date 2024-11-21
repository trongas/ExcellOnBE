using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client, int>
    {
        Task<Client> GetClientByEmail(string email);
        Task<ICollection<Client>> GetClientsByName(string firstName, string lastName);
    }

}

using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ClientRepository : GenericRepository<Client, int>, IClientRepository
    {
        public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger) : base(context, logger)
        {
        }

        public async Task<Client> GetClientByEmail(string email)
        {
            try
            {
                return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting client by email");
                throw; // You may want to handle the exception more gracefully based on your application's requirements.
            }
        }

        public override async Task<Client?> GetById(int id)
        {
            try
            {
                return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        public async Task<ICollection<Client>> GetClientsByName(string firstName, string lastName)
        {
            try
            {
                return await _context.Clients
                    .AsNoTracking()
                    .Where(c => c.FirstName == firstName && c.LastName == lastName)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting clients by name");
                throw; // You may want to handle the exception more gracefully based on your application's requirements.
            }
        }   
    }
}

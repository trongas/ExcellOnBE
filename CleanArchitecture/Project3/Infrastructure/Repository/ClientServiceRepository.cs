using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ClientServiceRepository : GenericRepository<ClientService, int>, IClientServiceRepository
    {
        public ClientServiceRepository(AppDbContext context, ILogger<ClientServiceRepository> logger) : base(context, logger)
        {
        }
        public override async Task<ClientService?> GetById(int id)
        {
            try
            {
                return await _context.ClientServices.AsNoTracking().FirstOrDefaultAsync(cs => cs.ClientServiceId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public async Task<int> GetTotalClientsByServiceId(int serviceId)
        {
            try
            {
                var totalClients = await _context.ClientServices
                    .AsNoTracking()
                    .Where(cs => cs.ServiceId == serviceId)
                    .CountAsync();

                return totalClients;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while getting total clients by ServiceId: {e}");
                return 0;
            }
        }
    }
}

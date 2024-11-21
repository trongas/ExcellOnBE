using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    internal class ServiceRepository : GenericRepository<Service, int>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context, ILogger<ServiceRepository> logger) : base(context, logger)
        {
        }

        public override async Task<Service?> GetById(int id)
        {
            try
            {
                return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.ServiceId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public async Task<Service?> GetByName(string name)
        {

            try
            {
                return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.ServiceName == name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ReportRepository : GenericRepository<Report, int>, IReportRepository
    {
        public ReportRepository(AppDbContext context, ILogger<ReportRepository> logger) : base(context, logger)
        {
        }
        public override async Task<Report?> GetById(int id)
        {
            try
            {
                return await _context.Reports.AsNoTracking().FirstOrDefaultAsync(r => r.ReportId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}

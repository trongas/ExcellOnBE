using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context, ILogger<EmployeeRepository> logger) : base(context, logger)
        {
        }

        public async Task<List<Employee>> GetByDepartmentId(int departmentId)
        {
            try
            {
                return await _context.Employees.AsNoTracking().Where(e => e.DepartmentId == departmentId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while trying to get employees by name.");
                return new List<Employee>();
            }
        }

        public async Task<Employee> GetByEmail(string email)
        {
            try
            {
                return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public override async Task<Employee?> GetById(int id)
        {
            try
            {
                return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public async Task<ICollection<Employee>> GetByName(string firstName, string lastName)
        {
            try
            {
                return await _context.Employees
                    .AsNoTracking()
                    .Where(e => e.FirstName == firstName || e.LastName == lastName)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while trying to get employees by name.");
                return new List<Employee>();
            }
        }

    }
}

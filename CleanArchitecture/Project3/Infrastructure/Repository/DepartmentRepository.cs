using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class DepartmentRepository : GenericRepository<Department, int>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context, ILogger<DepartmentRepository> logger) : base(context, logger)
        {
        }

        public async Task<bool> AddEmployee(Employee employee, int departmentId)
        {

            try
            {
                var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == departmentId);
                if (department == null)
                {
                    return false;
                }
                if (employee != null)
                {
                    department.Employees.Add(employee);

                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public override async Task<Department?> GetById(int id)
        {
            try
            {
                return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public async Task<Department?> GetByName(string name)
        {
            try
            {
                return await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.DepartmentName == name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}

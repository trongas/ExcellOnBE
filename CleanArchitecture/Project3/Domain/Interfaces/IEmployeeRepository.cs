using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee, int>
    {
        Task<Employee> GetByEmail(string email);
        Task<ICollection<Employee>> GetByName(string firstName, string lastName);
        Task<List<Employee>> GetByDepartmentId(int departmentId);
    }
}

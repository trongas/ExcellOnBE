using Application.DTOs.Employee;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandle : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetEmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var employee = await _unitOfWork.Employees.GetById(request.Id);
                if (employee == null)
                {
                    return null;
                }
                GetEmployeeDto employeeDto = new GetEmployeeDto
                {
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Id = request.Id,
                    Avatar = employee.Avatar,
                    DepartmentId = employee.DepartmentId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Dob = employee.Dob
                };
                return employeeDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}

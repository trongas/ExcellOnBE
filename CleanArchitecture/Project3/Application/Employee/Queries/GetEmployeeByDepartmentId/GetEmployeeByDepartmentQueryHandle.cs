using Application.DTOs.Employee;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Queries.GetEmployeeByDepartmentId
{
    internal class GetEmployeeByDepartmentQueryHandle : IRequestHandler<GetEmployeeByDepartmentQuery, List<GetEmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByDepartmentQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetEmployeeDto>> Handle(GetEmployeeByDepartmentQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var employees = await _unitOfWork.Employees.GetByDepartmentId(request.Id);
                List<GetEmployeeDto> employeesDto = new List<GetEmployeeDto>();
                foreach (var employee in employees)
                {
                    if (employee != null)
                    {
                        var employeeDto = new GetEmployeeDto()
                        {
                            Id = employee.EmployeeId,
                            Avatar = employee.Avatar,
                            DepartmentId = employee.DepartmentId,
                            Dob = employee.Dob,
                            Email = employee.Email,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Phone = employee.Phone
                        };
                        employeesDto.Add(employeeDto);
                    }
                }
                return employeesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }

        }
    }
}

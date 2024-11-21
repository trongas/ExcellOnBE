using Application.DTOs.Employee;
using Application.Employee.Queries.GetEmployeeById;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Queries.GetByName
{
    public class GetEmployeeByNameQueryHandle : IRequestHandler<GetEmployeeByNameQuery, ICollection<GetEmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByNameQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetEmployeeDto>> Handle(GetEmployeeByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _unitOfWork.Employees.GetByName(request.FirstName, request.LastName);
                var employeesDto = new List<GetEmployeeDto>();
                foreach (Domain.Entities.Employee e in employees)
                {
                    var employeeDto = new GetEmployeeDto()
                    {
                        Id = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Avatar = e.Avatar,
                        DepartmentId = e.DepartmentId,
                        Dob = e.Dob,
                        Email = e.Email,
                        Phone = e.Phone
                    };
                    employeesDto.Add(employeeDto);
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

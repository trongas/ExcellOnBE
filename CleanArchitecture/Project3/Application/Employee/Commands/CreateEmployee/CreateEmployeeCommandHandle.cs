using Application.DTOs.Employee;
using Domain.Abstraction;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Services;
using MediatR;

namespace Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandle : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public CreateEmployeeCommandHandle(
            IUnitOfWork unitOfWork,
            ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = request.EmployeeDto.Email;
                string filePath = "";
                var result = _cloudinaryService.Upload(request.EmployeeDto.Avatar, fileName);
                if (result != null)
                {
                    filePath = result.Url.ToString();
                }
                var newEmployee = new Domain.Entities.Employee()
                {
                    FirstName = request.EmployeeDto.FirstName,
                    LastName = request.EmployeeDto.LastName,
                    Avatar = filePath,
                    Email = request.EmployeeDto.Email,
                    Phone = request.EmployeeDto.Phone,
                    DepartmentId = request.EmployeeDto.DepartmentId,
                    Dob = request.EmployeeDto.Dob
                };
                if (request.EmployeeDto.Avatar == null)
                {
                    newEmployee.Avatar = "defautAvatar.png";
                }
                Console.WriteLine(newEmployee);
                await _unitOfWork.CompleteAsync();
                bool isCreate = await _unitOfWork.Employees.Add(newEmployee);
                if (isCreate)
                {
                    var employee = await _unitOfWork.Employees.GetByEmail(newEmployee.Email);
                    await _unitOfWork.CompleteAsync();
                    Console.Write(employee);
                    if (employee != null)
                    {

                        await _unitOfWork.Departments.AddEmployee(employee, employee.DepartmentId);
                        await _unitOfWork.CompleteAsync();
                        return request.EmployeeDto;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}


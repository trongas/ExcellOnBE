using Application.DTOs.Employee;
using Domain.Abstraction;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Impl;
using MediatR;

namespace Application.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandle : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public UpdateEmployeeCommandHandle(
            IUnitOfWork unitOfWork,
            ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<UpdateEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = request.UpdateEmployeeDto.Email;
                string filePath = "";
                var result = _cloudinaryService.Upload(request.UpdateEmployeeDto.Avatar, fileName);
                if (result != null)
                {
                    filePath = result.Url.ToString();
                }
                var exitingEmployee = await _unitOfWork.Employees.GetById(request.UpdateEmployeeDto.Id);

                if (exitingEmployee != null)
                {
                    var updateEmployee = new Domain.Entities.Employee()
                    {
                        EmployeeId = request.UpdateEmployeeDto.Id,
                        FirstName = request.UpdateEmployeeDto.FirstName,
                        LastName = request.UpdateEmployeeDto.LastName,
                        Avatar = filePath,
                        Email = request.UpdateEmployeeDto.Email,
                        Phone = request.UpdateEmployeeDto.Phone,
                        DepartmentId = request.UpdateEmployeeDto.DepartmentId,
                        Dob = request.UpdateEmployeeDto.Dob
                    };
                    await _unitOfWork.Employees.Update(updateEmployee);
                    await _unitOfWork.Departments.
                        AddEmployee(updateEmployee, request.UpdateEmployeeDto.DepartmentId);
                    await _unitOfWork.CompleteAsync();
                    return request.UpdateEmployeeDto;
                }
                else
                {
                    throw new Exception("Employee does not exit !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

using Application.DTOs.Employee;
using Domain.Abstraction;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandle : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteEmployeeCommandHandle(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employeeDeleted = await _unitOfWork.Employees.GetById(request.EmployeeId);
                if (employeeDeleted != null)
                {
                    await _unitOfWork.Employees.Delete(employeeDeleted);
                    var userDelete = await _userManager.FindByEmailAsync(employeeDeleted.Email);
                    if (userDelete != null)
                    {
                        await _userManager.DeleteAsync(userDelete);
                    }
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

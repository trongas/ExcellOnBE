using Application.DTOs.Employee;
using Application.Employee.Commands.DeleteEmployee;
using Application.Employee.Commands.UpdateEmployee;
using Application.Employee.Queries.GetAllEmployee;
using Application.Employee.Queries.GetByName;
using Application.Employee.Queries.GetEmployeeByDepartmentId;
using Application.Employee.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeeController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeeQuery = new GetAllEmployeeQuery();
            var employees = await _mediator.Send(employeeQuery);
            return Ok(employees);
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var query = new GetEmployeeByIdQuery()
            {
                Id = id
            };
            var employee = await _mediator.Send(query);
            if (employee != null)
            {

                return Ok(employee);
            }
            return BadRequest($"Employee {id} not found!");

        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetEmployeeByName([FromQuery] string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    // Xử lý khi name là null hoặc rỗng
                    string firstName = name.Split(' ')[0];
                    string lastName = " ";
                    if (!(name.Split(' ').Length > 1))
                    {
                        lastName = name.Split(' ')[1];
                    }
                    var query = new GetEmployeeByNameQuery()
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };

                    var employee = await _mediator.Send(query);

                    if (employee != null)
                    {
                        return Ok(employee);
                    }
                    else
                    {
                        return NotFound($"Employee {name} not found.");
                    }
                }
                return BadRequest("Name cannot be null or empty.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetByDepartmentId(int departmentId)
        {
            try
            {
                var query = new GetEmployeeByDepartmentQuery()
                {
                    Id = departmentId
                };
                var employees = await _mediator.Send(query);
                if (employees != null)
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound($"Department with ID {departmentId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmp([FromForm] UpdateEmployeeDto request)
        {
            try
            {
                if (request.Id != 0)
                {
                    var command = new UpdateEmployeeCommand()
                    {
                        UpdateEmployeeDto = request
                    };
                    var employee = await _mediator.Send(command);
                    if (employee != null)
                    {
                        return Ok(new { Employee = employee, Message = "Update success!" });
                    }
                    return BadRequest("Something wrong please try again!");
                }
                else
                {
                    return BadRequest("Invalid ClientId");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var command = new DeleteEmployeeCommand()
                {
                    EmployeeId = id
                };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

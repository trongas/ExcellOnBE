using Application.Department.Commands.CreateDepartment;
using Application.Department.Commands.DeleteDepartment;
using Application.Department.Commands.UpdateDepartment;
using Application.Department.Queries.GetAllDepartment;
using Application.DTOs.Department;
using Application.DTOs.Employee;
using Application.Employee.Commands.DeleteEmployee;
using Application.Employee.Commands.UpdateEmployee;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class DepartmentController : ControllerBase
    {
        public readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var query = new GetAllDepartmentQuery();
            var departments = await _mediator.Send(query);
            return Ok(departments);
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentDto request)
        {
            //Validate
            if (request == null)
            {
                return BadRequest("Invalid info");
            }

            var command = new CreateDepartmentCommand()
            {
                CreateDepartment = request
            };
            var department = await _mediator.Send(command);
            return Ok(department);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto request)
        {
            try
            {
                if (request.Id != 0)
                {
                    var command = new UpdateDepartmentCommand()
                    {
                        UpdateDepartmentDto = request
                    };
                    var department = await _mediator.Send(command);
                    if (department != null)
                    {

                        return Ok(new { Department = department, Message = "Update success!" });
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
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var command = new DeleteDepartmentCommand()
                {
                    Id = id
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

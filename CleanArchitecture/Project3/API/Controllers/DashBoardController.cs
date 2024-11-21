using Application.Client.Queries.GetTotalClient;
using Application.ClientService.Queries.GetClientByServiceId;
using Application.Department.Queries.GetTotalDepartment;
using Application.Employee.Queries.GetTotalEmployee;
using Application.Payment.Queries.GetPaymentByYear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        public readonly IMediator _mediator;
        public DashBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("total-employee")]
        public async Task<IActionResult> GetTotalEmployee()
        {
            var query = new GetTotalEmployeeQuery();
            var totalEmployee = await _mediator.Send(query);
            return Ok(totalEmployee);
        }

        [HttpGet("client-count")]
        public async Task<IActionResult> GetClientCount()
        {
            var query = new GetTotalClientQuery();
            var totalClient = await _mediator.Send(query);
            return Ok(totalClient);
        }

        [HttpGet("department-count")]
        public async Task<IActionResult> GetDepartmentCount()
        {
            var query = new GetTotalDepartmentQuery();
            var totalDepartment = await _mediator.Send(query);
            return Ok(totalDepartment);
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue([FromQuery] int year)
        {
            var query = new GetPaymentByYearQuery()
            {
                Year = year
            };
            var revenues = await _mediator.Send(query);
            if (revenues.Any())
            {
                return Ok(revenues);
            }
            return BadRequest("HihiHaha");
        }

        [HttpGet("total-client-of-service")]
        public async Task<IActionResult> GetTotalClientOfService([FromQuery] int serviceId)
        {
            var query = new GetTotalClientByServiceIdQuery()
            {
                ServiceId = serviceId
            };
            var totalClient = await _mediator.Send(query);
            if (totalClient > 0)
            {
                return Ok(totalClient);
            }
            return BadRequest("Something wrong !");
        }
    }
}

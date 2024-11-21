using Application.DTOs.Service;
using Application.Service.Commands.CreateService;
using Application.Service.Commands.DeleteService;
using Application.Service.Commands.UpdateService;
using Application.Service.Queries.GetAllService;
using Application.Service.Queries.GetServiceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var serviceQuery = new GetAllServiceQuery();
            var services = await _mediator.Send(serviceQuery);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var query = new GetServiceByIdQuery()
            {
                ServiceId = id
            };
            var service = await _mediator.Send(query);
            if (service != null)
            {
                return Ok(service);
            }
            return NotFound($"Service {id} not found!");
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto createServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createServiceCommand = new CreateServiceCommand { ServiceDto = createServiceDto };

            try
            {
                var createdService = await _mediator.Send(createServiceCommand);

                if (createdService == null)
                {
                    return StatusCode(500, "Internal server error: Unable to create the service");
                }
                return CreatedAtAction(nameof(GetAllServices), createdService);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceDto updateServiceDto)
        {
            try
            {
                var updateCommand = new UpdateServiceCommand
                {
                    UpdateServiceDto = updateServiceDto
                };

                var updatedService = await _mediator.Send(updateCommand);

                if (updatedService != null)
                {
                    return Ok(updatedService);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while updating the service.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var command = new DeleteServiceCommand()
            {
                ServiceId = id
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"Service {id} not found!");
        }
    }
}

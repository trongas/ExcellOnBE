using Application.ClientService.Commands.CreateClientService;
using Application.ClientService.Commands.DeleteClientService;
using Application.ClientService.Commands.UpdateClientService;
using Application.ClientService.Queries.GetAllClientService;
using Application.ClientService.Queries.GetClientServiceById;
using Application.DTOs.ClientService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ClientServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientServices()
        {
            var clientServiceQuery = new GetAllClientServiceQuery();
            var clientServices = await _mediator.Send(clientServiceQuery);
            return Ok(clientServices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientServiceById(int id)
        {
            var query = new GetClientServiceByIdQuery
            {
                ClientServiceId = id
            };
            var clientService = await _mediator.Send(query);
            if (clientService != null)
            {
                return Ok(clientService);
            }
            return NotFound($"ClientService {id} not found!");
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientService([FromBody] CreateClientServiceDto createClientServiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createClientServiceCommand = new CreateClientServiceCommand
            {
                ClientServiceDto = createClientServiceDto
            };

            try
            {
                var createdClientService = await _mediator.Send(createClientServiceCommand);

                if (createdClientService == null)
                {
                    return StatusCode(500, "Internal server error: Unable to create the client service");
                }
                return CreatedAtAction(nameof(GetAllClientServices), createdClientService);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientService([FromBody] UpdateClientServiceDto updateClientServiceDto)
        {
            try
            {
                var updateCommand = new UpdateClientServiceCommand
                {
                    UpdateClientServiceDto = updateClientServiceDto
                };

                var updatedClientService = await _mediator.Send(updateCommand);

                if (updatedClientService != null)
                {
                    return Ok(updatedClientService);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while updating the client service.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientService(int id)
        {
            var command = new DeleteClientServiceCommand
            {
                ClientServiceId = id
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"ClientService {id} not found!");
        }
    }
}

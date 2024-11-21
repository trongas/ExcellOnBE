
using Application.DTOs.ServiceCharges;
using Application.ServiceCharges.Commands.CreateServiceCharges;
using Application.ServiceCharges.Commands.DeleteServiceCharges;
using Application.ServiceCharges.Commands.UpdateServiceCharges;
using Application.ServiceCharges.Queries.GetAllServiceCharges;
using Application.ServiceCharges.Queries.GetServiceChargesById;
using Application.ServiceCharges.Queries.GetServiceChargesByServiceId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ServiceChargesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceChargesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServiceCharges()
        {
            var query = new GetAllServiceChargesQuery();
            var serviceCharges = await _mediator.Send(query);
            return Ok(serviceCharges);
        }

        [HttpGet]
        [Route("getByServiceId/{serviceId}")]
        public async Task<IActionResult> GetByServiceId(int serviceId)
        {
            try
            {
                var query = new GetServiceChargeByServiceIdQuery()
                {
                    Id = serviceId
                };
                var serviceCharges = await _mediator.Send(query);
                if (serviceCharges != null)
                {
                    return Ok(serviceCharges);
                }
                else
                {
                    return NotFound($"Service with ID {serviceId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceChargeById(int id)
        {
            var query = new GetServiceChargesByIdQuery
            {
                ServiceChargesId = id
            };
            var serviceCharge = await _mediator.Send(query);
            if (serviceCharge != null)
            {
                return Ok(serviceCharge);
            }
            return NotFound($"ServiceCharge {id} not found!");
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceCharge([FromBody] CreateServiceChargesDto createServiceChargeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createCommand = new CreateServiceChargesCommand
            {
                ServiceChargesDto = createServiceChargeDto
            };

            try
            {
                var createdServiceCharge = await _mediator.Send(createCommand);

                if (createdServiceCharge == null)
                {
                    return StatusCode(500, "Internal server error: Unable to create the service charge");
                }
                return CreatedAtAction(nameof(GetAllServiceCharges), createdServiceCharge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceCharge([FromBody] UpdateServiceChargesDto updateServiceChargeDto)
        {
            try
            {
                var updateCommand = new UpdateServiceChargesCommand
                {
                    UpdateServiceChargesDto = updateServiceChargeDto
                };

                var updatedServiceCharge = await _mediator.Send(updateCommand);

                if (updatedServiceCharge != null)
                {
                    return Ok(updatedServiceCharge);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while updating the service charge.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceCharge(int id)
        {
            var command = new DeleteServiceChargesCommand
            {
                ServiceChargesId = id
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"ServiceCharge {id} not found!");
        }
    }
}

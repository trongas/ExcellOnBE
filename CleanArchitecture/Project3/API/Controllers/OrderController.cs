
using Application.Cart.Queries.GetCartByClientId;
using Application.DTOs.Order;
using Application.DTOs.OrderDetail;
using Application.Order.Commands.AddOrder;
using Application.Order.Commands.DeleteOrder;
using Application.Order.Queries.GetOrder;
using Application.Order.Queries.GetOrderByClientId;
using Application.Order.Queries.GetOrderById;
using Application.OrderDetail.Commands.CreateOrderDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] AddOrderDto order)
        {
            try
            {
                var command = new AddOrderCommand
                {
                    AddOrderDto = order
                };

                int orderId = await _mediator.Send(command);

                if (orderId != -1)
                {
                    return Ok($"Order successfully created. Order ID: {orderId}");
                }
                else
                {
                    return StatusCode(500, "Failed to create order.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("order-detail")]
        public async Task<IActionResult> AddOrderDetail([FromBody] AddOrderDetailDto orderItem)
        {
            try
            {
                var command = new CreateOrderDetailCommand
                {
                    AddOrderDetailDto = orderItem
                };

                await _mediator.Send(command);

                return Ok("Order successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getByClientId/{clientId}")]
        public async Task<IActionResult> GetByClientId(int clientId)
        {
            try
            {
                var query = new GetOrderByClientIdQuery()
                {
                    ClientId = clientId
                };
                var cart = await _mediator.Send(query);
                if (cart != null)
                {
                    return Ok(cart);
                }
                else
                {
                    return NotFound($"Client with ID {clientId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var orderQuery = new GetOrderQuery();
            var orders = await _mediator.Send(orderQuery);
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GerOrderById(int id)
        {
            var query = new GetOrderByIdQuery()
            {
                OrderId = id
            };
            var order = await _mediator.Send(query);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound($"Order {id} not found!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand()
            {
                OrderId = id
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"Order {id} not found!");
        }
    }
}

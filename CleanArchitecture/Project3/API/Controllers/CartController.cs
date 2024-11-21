using Application.Cart.Commands.AddCart;
using Application.Cart.Commands.DeleteCartByClientId;
using Application.Cart.Commands.DeleteCartItem;
using Application.Cart.Queries.GetCartByClientId;
using Application.Cart.Queries.GetCartId;
using Application.DTOs.Cart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart( [FromBody] AddToCartDto cartItem)
        {
            try
            {
                var command = new AddToCartCommand
                {
                   AddToCartDto = cartItem
                };

                await _mediator.Send(command);

                return Ok("Item added to cart successfully.");
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
                var query = new GetCartByClientIdQuery()
                {
                    Id = clientId
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
        [HttpGet("GetCartId")]
        public async Task<IActionResult> GetCartId()
        {
            var cartQuery = new GetCartIdQuery();
            var carts = await _mediator.Send(cartQuery);
            return Ok(carts);
        }
        //[HttpGet("total-price/{clientId}")]
        //public async Task<IActionResult> GetTotalPrice(int clientId)
        //{
        //    var query = new GetTotalPriceQuery();
        //    var totalItem = await _mediator.Send(query);
        //    return Ok(totalItem);
        //}

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCartItem(int cartId)
        {
            var command = new DeleteCartItemCommand
            {
                CartId = cartId,
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"ServiceCharge {cartId} not found!");
        }
        [HttpDelete("client/{clientId}")]
        public async Task<IActionResult> DeleteCartByClientId(int clientId)
        {
            var command = new DeleteCartByClientIdCommand
            {
                ClientId = clientId,
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"ServiceCharge {clientId} not found!");
        }
    }
}

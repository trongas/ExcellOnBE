using Application.DTOs.Payment;
using Application.Payment.Commands.CreatePayment;
using Application.Payment.Commands.UpdatePaymentCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public PaymentController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }
        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentIntentDto paymentIntentDto)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var options = new PaymentIntentCreateOptions
            {
                Amount = paymentIntentDto.Amount,
                Currency = paymentIntentDto.Currency,
            };
            var command = new CreatePaymentCommand()
            {
                PaymentIntentDto = paymentIntentDto
            };
            await _mediator.Send(command);
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new { ClientSecret = paymentIntent.ClientSecret });
        }

        [HttpPost("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment([FromBody] ConfirmPaymentDto confirmPaymentDto)
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                var service = new PaymentIntentService();

                var options = new PaymentIntentConfirmOptions
                {
                    PaymentMethod = confirmPaymentDto.TokenId,
                    ClientSecret = confirmPaymentDto.ClientSecret,

                };
                var command = new UpdatePaymentCommand()
                {
                    Status = "Confirm"
                };
                await _mediator.Send(command);
                var paymentIntent = await service.ConfirmAsync(confirmPaymentDto.ClientSecret, options);

                return Ok(new { PaymentStatus = paymentIntent.Status });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}

using Application.DTOs.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<bool>
    {
        public PaymentIntentDto PaymentIntentDto { get; set; }
    }
}

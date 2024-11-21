using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Commands.UpdatePaymentCommand
{
    public class UpdatePaymentCommand : IRequest<bool>
    {
        public int PaymentId { get; set; }
        public string Status { get; set; }
    }
}

using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Commands.UpdatePaymentCommand
{
    public class UpdatePaymentCommandHandle : IRequestHandler<UpdatePaymentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePaymentCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.Payments.GetById(request.PaymentId);
            if (payment != null)
            {
                payment.Status = request.Status;
                await _unitOfWork.Payments.Update(payment);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

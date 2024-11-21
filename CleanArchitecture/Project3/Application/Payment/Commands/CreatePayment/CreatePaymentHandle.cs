using Domain.Abstraction;
using MediatR;

namespace Application.Payment.Commands.CreatePayment
{
    public class CreatePaymentHandle : IRequestHandler<CreatePaymentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePaymentHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Domain.Entities.Payment()
            {
                Amount = request.PaymentIntentDto.Amount,
                ClientId = request.PaymentIntentDto.ClientId,
                PaymentDate = DateTime.Now,
                CreatedAt = DateTime.Now,
                Status = "Wait for pay"
            };
            await _unitOfWork.Payments.Add(payment);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}

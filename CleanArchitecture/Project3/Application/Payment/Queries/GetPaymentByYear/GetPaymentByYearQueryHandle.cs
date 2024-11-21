using Application.DTOs.Payment;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Payment.Queries.GetPaymentByYear
{
    public class GetPaymentByYearQueryHandle : IRequestHandler<GetPaymentByYearQuery, List<RevenueDto>>
    {
        public IUnitOfWork _unitOfWork;
        public GetPaymentByYearQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RevenueDto>> Handle(GetPaymentByYearQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Payment> payments = await _unitOfWork.Payments.GetPaymentsByYear(request.Year);
            List<RevenueDto> revenueDtos = new List<RevenueDto>();
            foreach (var payment in payments)
            {
                if (payment != null)
                {
                    var revenueDto = new RevenueDto()
                    {
                        Amount = payment.Amount,
                        PaymentDate = payment.PaymentDate
                    };
                    revenueDtos.Add(revenueDto);
                }
            }
            return revenueDtos;
        }
    }
}

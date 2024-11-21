using Application.DTOs.Payment;
using MediatR;

namespace Application.Payment.Queries.GetPaymentByYear
{
    public class GetPaymentByYearQuery : IRequest<List<RevenueDto>>
    {
        public int Year { get; set; }
    }
}

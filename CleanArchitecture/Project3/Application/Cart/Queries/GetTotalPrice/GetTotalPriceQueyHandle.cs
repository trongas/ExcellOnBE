using Application.DTOs.ServiceCharges;
using Azure.Core;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetTotalPrice
{
    public class GetTotalPriceQueyHandle : IRequestHandler<GetTotalPriceQuery, double>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalPriceQueyHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<double> Handle(GetTotalPriceQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Cart.CalculateTotalPrice(request.Id);
        }
    }
}

using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetTotalClient
{
    public class GetTotalClientQueryHandle : IRequestHandler<GetTotalClientQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalClientQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(GetTotalClientQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Clients.Count();
        }
    }
}


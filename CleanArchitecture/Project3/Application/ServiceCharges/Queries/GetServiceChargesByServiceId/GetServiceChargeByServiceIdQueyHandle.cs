using Application.DTOs.ServiceCharges;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceCharges.Queries.GetServiceChargesByServiceId
{
    internal class GetServiceChargeByServiceIdQueyHandle : IRequestHandler<GetServiceChargeByServiceIdQuery, List<GetServiceChargesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetServiceChargeByServiceIdQueyHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetServiceChargesDto>> Handle(GetServiceChargeByServiceIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceCharges = await _unitOfWork.ServicesCharges.GetByServiceId(request.Id);
                List<GetServiceChargesDto> serviceChargesDto = new List<GetServiceChargesDto>();

                foreach (var serviceCharge in serviceCharges)
                {
                    if (serviceCharge != null)
                    {
                        var serviceChargeDto = new GetServiceChargesDto()
                        {
                            ServiceChargeId = serviceCharge.ServiceChargesId,
                            ServiceChargesName = serviceCharge.ServiceChargesName,
                            Price = serviceCharge.Price,
                            ServiceChargesDescription = serviceCharge.ServiceChargesDescription,
                            ServiceId = serviceCharge.ServiceId
                        };
                        serviceChargesDto.Add(serviceChargeDto);
                    }
                }
                return serviceChargesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query: " + ex);
                return null;
            }
        }
    }
}

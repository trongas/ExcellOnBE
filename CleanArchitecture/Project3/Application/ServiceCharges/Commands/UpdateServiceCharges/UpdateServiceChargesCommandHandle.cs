using Application.DTOs.ServiceCharges;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceCharges.Commands.UpdateServiceCharges
{
    public class UpdateServiceChargesCommandHandle : IRequestHandler<UpdateServiceChargesCommand, UpdateServiceChargesDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceChargesCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateServiceChargesDto> Handle(UpdateServiceChargesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingServiceCharge = await _unitOfWork.ServicesCharges.GetById(request.UpdateServiceChargesDto.ServiceChargesId);

                if (existingServiceCharge == null)
                {
                    // Handle the case where the service charge to be updated is not found
                    return null;
                }

                existingServiceCharge.ServiceChargesName = request.UpdateServiceChargesDto.ServiceChargesName;
                existingServiceCharge.ServiceChargesDescription = request.UpdateServiceChargesDto.ServiceChargesDescription;
                existingServiceCharge.Price = request.UpdateServiceChargesDto.Price;

                // Update the service charge in the repository
                bool isUpdate = await _unitOfWork.ServicesCharges.Update(existingServiceCharge);

                if (isUpdate)
                {
                    await _unitOfWork.CompleteAsync();
                    // If the update is successful, you may return the updated DTO or any relevant information
                    return request.UpdateServiceChargesDto;
                }

                // If the update fails, you might want to handle it accordingly
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Handle the exception as needed, log, and return null or throw a custom exception
                return null;
            }
        }
    }
}

using Application.DTOs.ClientService;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Commands.UpdateClientService
{
    public class UpdateClientServiceCommandHandle : IRequestHandler<UpdateClientServiceCommand, UpdateClientServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientServiceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateClientServiceDto> Handle(UpdateClientServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingClientService = await _unitOfWork.ClientServices.GetById(request.UpdateClientServiceDto.ClientServiceId);

                if (existingClientService == null)
                {
                    // Handle the case where the client service to be updated is not found
                    return null;
                }

                // Update the existingClientService with the new values from the request
                existingClientService.Status = request.UpdateClientServiceDto.Status;
                existingClientService.ExpiredDay = request.UpdateClientServiceDto.ExpiredDay;
                existingClientService.ServiceId = request.UpdateClientServiceDto.ServiceId;
                existingClientService.ClientId = request.UpdateClientServiceDto.ClientId;
                existingClientService.StartDay = request.UpdateClientServiceDto.StartDay;



                // Update the client service in the repository
                bool isUpdate = await _unitOfWork.ClientServices.Update(existingClientService);

                if (isUpdate)
                {
                    await _unitOfWork.CompleteAsync();
                    // If the update is successful, you may return the updated DTO or any relevant information
                    return request.UpdateClientServiceDto;
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

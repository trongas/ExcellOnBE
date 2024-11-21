using Application.DTOs.Service;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Service.Commands.UpdateService
{
    public class UpdateServiceCommandHandle : IRequestHandler<UpdateServiceCommand, UpdateServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateServiceDto> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingService = await _unitOfWork.Services.GetById(request.UpdateServiceDto.ServiceId);

                if (existingService == null)
                {
                    // Handle the case where the service to be updated is not found
                    return null;
                }

                // Update the existingService with the new values from the request
                existingService.ServiceName = request.UpdateServiceDto.ServiceName;
                existingService.Description = request.UpdateServiceDto.Description;
                existingService.TotalDay = request.UpdateServiceDto.TotalDay;

                // Update the service in the repository
                bool isUpdate = await _unitOfWork.Services.Update(existingService);

                if (isUpdate)
                {
                    await _unitOfWork.CompleteAsync();
                    // If the update is successful, you may return the updated DTO or any relevant information
                    return request.UpdateServiceDto;
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

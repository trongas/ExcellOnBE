using Domain.Abstraction;
using MediatR;

namespace Application.Service.Commands.DeleteService
{
    public class DeleteServiceCommandHandle : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceDeleted = await _unitOfWork.Services.GetById(request.ServiceId);
                if (serviceDeleted != null)
                {
                    await _unitOfWork.Services.Delete(serviceDeleted);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}

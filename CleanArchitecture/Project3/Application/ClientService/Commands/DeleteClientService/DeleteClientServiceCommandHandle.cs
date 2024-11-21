using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.ClientService.Commands.DeleteClientService
{
    public class DeleteClientServiceCommandHandle : IRequestHandler<DeleteClientServiceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientServiceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteClientServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientServiceDeleted = await _unitOfWork.ClientServices.GetById(request.ClientServiceId);
                if (clientServiceDeleted != null)
                {
                    await _unitOfWork.ClientServices.Delete(clientServiceDeleted);
                    await _unitOfWork.CompleteAsync();
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

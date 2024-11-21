using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceCharges.Commands.DeleteServiceCharges
{
    public class DeleteServiceChargesCommandHandle : IRequestHandler<DeleteServiceChargesCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceChargesCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteServiceChargesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var serviceChargeToDelete = await _unitOfWork.ServicesCharges.GetById(request.ServiceChargesId);
                if (serviceChargeToDelete != null)
                {
                    await _unitOfWork.ServicesCharges.Delete(serviceChargeToDelete);
                    // Additional logic specific to service charge deletion can be added here

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

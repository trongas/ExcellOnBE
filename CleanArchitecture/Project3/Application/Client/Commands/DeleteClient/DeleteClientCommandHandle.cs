using Domain.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommandHandle : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public DeleteClientCommandHandle(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientToDelete = await _unitOfWork.Clients.GetById(request.ClientId);
                if (clientToDelete != null)
                {
                    await _unitOfWork.Clients.Delete(clientToDelete);
                    var userDeleled = await _userManager.FindByEmailAsync(clientToDelete.Email);
                    if (userDeleled != null)
                    {
                        await _userManager.DeleteAsync(userDeleled);
                    }
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

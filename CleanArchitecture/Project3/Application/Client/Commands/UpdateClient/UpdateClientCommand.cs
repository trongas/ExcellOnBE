using Application.DTOs.Client;
using MediatR;

namespace Application.Client.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<UpdateClientDto>
    {
      
        public UpdateClientDto UpdateClientDto { get; set; }

       
    }
}

using Application.DTOs.ClientService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ClientService.Commands.UpdateClientService
{
    public class UpdateClientServiceCommand : IRequest<UpdateClientServiceDto>
    {
        public UpdateClientServiceDto UpdateClientServiceDto { get; set; }
    }
}

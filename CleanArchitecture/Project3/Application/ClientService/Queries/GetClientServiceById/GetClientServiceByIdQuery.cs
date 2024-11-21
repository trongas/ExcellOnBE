using Application.DTOs.ClientService;
using MediatR;

namespace Application.ClientService.Queries.GetClientServiceById
{
    public class GetClientServiceByIdQuery : IRequest<GetClientServiceDto>
    {
        public int ClientServiceId { get; set; }
    }
}

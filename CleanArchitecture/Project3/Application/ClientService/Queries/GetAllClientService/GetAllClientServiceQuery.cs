using Application.DTOs.ClientService;
using MediatR;

namespace Application.ClientService.Queries.GetAllClientService
{
    public class GetAllClientServiceQuery : IRequest<ICollection<GetClientServiceDto>>
    {
    }
}

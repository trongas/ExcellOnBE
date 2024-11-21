using Application.DTOs.Service;
using MediatR;

namespace Application.Service.Queries.GetAllService
{
    public class GetAllServiceQuery : IRequest<ICollection<GetServiceDto>>
    {
    }
}

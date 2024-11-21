using Application.DTOs.Service;
using MediatR;

namespace Application.Service.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<GetServiceDto>
    {
        public int ServiceId { get; set; }
    }
}

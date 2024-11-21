using MediatR;

namespace Application.ClientService.Queries.GetClientByServiceId
{
    public class GetTotalClientByServiceIdQuery : IRequest<int>
    {
        public int ServiceId { get; set; }
    }
}

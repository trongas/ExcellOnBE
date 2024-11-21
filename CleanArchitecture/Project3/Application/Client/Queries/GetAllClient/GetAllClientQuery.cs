using Application.DTOs.Client;
using MediatR;

namespace Application.Client.Queries.GetAllClient
{
    public class GetAllClientQuery : IRequest<ICollection<GetClientDto>>
    {
    }
}

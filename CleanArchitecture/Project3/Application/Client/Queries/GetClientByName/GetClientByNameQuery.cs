using Application.DTOs.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetClientByName
{
    public class GetClientByNameQuery : IRequest<ICollection<GetClientDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using Application.DTOs.Cart;
using Application.DTOs.Client;
using Application.DTOs.Employee;
using Application.DTOs.Service;
using Application.DTOs.ServiceCharges;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Client.Queries.GetAllClient
{
    public class GetAllClientQueryHandle : IRequestHandler<GetAllClientQuery, ICollection<GetClientDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllClientQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetClientDto>> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
        {
            var clients = await _unitOfWork.Clients.All();
            var clientDtos = new List<GetClientDto>();

            foreach (Domain.Entities.Client c in clients)
            {
               
               var clientDto = new GetClientDto
                {
                    ClientId = c.ClientId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Dob = c.Dob,
                    Email = c.Email,
                    Phone = c.Phone,
                   
                };              

                clientDtos.Add(clientDto);
            }

            return clientDtos;
           
        }
    }
}

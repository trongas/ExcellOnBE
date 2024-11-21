using Application.DTOs.Cart;
using Application.DTOs.Client;
using Application.DTOs.Employee;
using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.Queries.GetClientById
{
    public class GetClientByIdQueryHandle : IRequestHandler<GetClientByIdQuery, GetClientDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientByIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _unitOfWork.Clients.GetById(request.Id);
                if (clients == null)
                {
                    return null;
                }

                GetClientDto clientDtos = new GetClientDto
                {
                    ClientId = clients.ClientId,
                    FirstName = clients.FirstName,
                    LastName = clients.LastName,
                    Dob = clients.Dob,
                    Email = clients.Email,
                    Phone = clients.Phone,

                };

                return clientDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}

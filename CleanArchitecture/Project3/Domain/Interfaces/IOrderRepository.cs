using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        Task<List<Order>> GetOrderByClientId(int clientId);
    }
}

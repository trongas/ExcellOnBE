using Domain.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartDetail, int>
    {
        Task<List<CartDetail>> GetCartById(int clientId);
        Task DeleteCartByClientId(int clientId);
        Task<CartDetail> GetByServiceChargeId(int serviceChargeId);
        Task<double> CalculateTotalPrice(int id);

    }
}

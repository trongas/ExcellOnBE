using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<Order>> GetOrderByClientId(int clientId)
        {
            try
            {
                return await _context.Order.AsNoTracking().Where(c => c.ClientId == clientId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while getting cart by ID: {Id}", clientId);
                return null;
            }
        }
    }
}

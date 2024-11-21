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
    public class OrderDetailRepository : GenericRepository<OrderDetail, int>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<ICollection<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                return await _context.OrderDetail.AsNoTracking().Where(c => c.OrderId == orderId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while getting cart by ID: {Id}", orderId);
                return null;
            }
        }
    }
}

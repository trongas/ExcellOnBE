using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly AuthDbContext _authDbContext;

        public IClientRepository Clients { get; set; }
        public IClientServiceRepository ClientServices { get; set; }
        public IDepartmentRepository Departments { get; set; }
        public IEmployeeRepository Employees { get; set; }
        public IPaymentRepository Payments { get; set; }
        public IReportRepository Reports { get; set; }
        public IServiceRepository Services { get; set; }
        public IServiceChargesRepository ServicesCharges { get; set; }
        public ICartRepository Cart { get; set; }
        public IOrderRepository Order { get; set; }
        public IOrderDetailRepository OrderDetail { get; set; }
        public UnitOfWork(
            AppDbContext context,
            AuthDbContext authDbContext,
            ILoggerFactory loggerFactory
           
           )
        {
            _context = context;
            _authDbContext = authDbContext;
       
            Clients = new ClientRepository(_context, loggerFactory.CreateLogger<ClientRepository>());
            ClientServices = new ClientServiceRepository(_context, loggerFactory.CreateLogger<ClientServiceRepository>());
            Departments = new DepartmentRepository(_context, loggerFactory.CreateLogger<DepartmentRepository>());
            Employees = new EmployeeRepository(_context, loggerFactory.CreateLogger<EmployeeRepository>());
            Payments = new PaymentRepository(_context, loggerFactory.CreateLogger<PaymentRepository>());
            Reports = new ReportRepository(_context, loggerFactory.CreateLogger<ReportRepository>());
            Services = new ServiceRepository(_context, loggerFactory.CreateLogger<ServiceRepository>());
            ServicesCharges = new ServiceChargesRepository(_context, loggerFactory.CreateLogger<ServiceChargesRepository>());
            Cart = new CartRepository(_context, loggerFactory.CreateLogger<CartRepository>());
            Order = new OrderRepository(_context, loggerFactory.CreateLogger<OrderRepository>());
            OrderDetail = new OrderDetailRepository(_context, loggerFactory.CreateLogger<OrderDetailRepository>());
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
            await _authDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

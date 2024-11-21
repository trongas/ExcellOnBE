using Domain.Interfaces;
using Domain.Repositories;

namespace Domain.Abstraction
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }

        ICartRepository Cart { get; }
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }
        IClientServiceRepository ClientServices { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IPaymentRepository Payments { get; }
        IReportRepository Reports { get; }
        IServiceRepository Services { get; }
        IServiceChargesRepository ServicesCharges { get; }
        Task CompleteAsync();
    }
}

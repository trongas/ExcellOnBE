using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientService> ClientServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCharges> ServiceCharges { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.ClientServices)
                .WithOne(cs => cs.Client)
                .HasForeignKey(cs => cs.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<ClientService>()
                .HasOne(cs => cs.Client)
                .WithMany(c => c.ClientServices)
                .HasForeignKey(cs => cs.ClientId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<Service>()
                .HasMany(s => s.ServicesCharges)
                .WithOne(sc => sc.Service)
                .HasForeignKey(sc => sc.ServiceId);

            modelBuilder.Entity<ServiceCharges>()
                .HasOne(sc => sc.Service)
                .WithMany(s => s.ServicesCharges)
                .HasForeignKey(sc => sc.ServiceId);
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Client)
                .WithMany(c => c.CartDetail)
                .HasForeignKey(cd => cd.ClientId);
            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.ServiceCharges)
                .WithMany(sc => sc.CartDetail)
                .HasForeignKey(cd => cd.ServiceChargeId);
            modelBuilder.Entity<Order>()
               .HasMany(o => o.OrderDetails)
               .WithOne(od => od.Order)
               .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<OrderDetail>()
               .HasOne(od => od.Order)
               .WithMany(o => o.OrderDetails)
               .HasForeignKey(od => od.OrderId);          
            base.OnModelCreating(modelBuilder);
        }
    }
}

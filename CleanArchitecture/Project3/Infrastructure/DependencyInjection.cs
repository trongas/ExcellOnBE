using Domain.Abstraction;
using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Infrastructure.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInFrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            services.AddDbContext<AuthDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("AuthConnection"),
                b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);

      

            //services.AddSingleton<IMongoClient>(provider =>
            //{
            //    var connectionString = configuration.GetConnectionString("ChatDBConnection");
            //    return new MongoClient(connectionString);
                
            //});

            //services.AddScoped<IMongoDatabase>(provider =>
            //{
            //    var client = provider.GetService<IMongoClient>();
            //    return client.GetDatabase("chat");
            //});

            services.AddLogging();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientServiceRepository, ClientServiceRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IServiceChargesRepository, ServiceChargesRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ChatGroupRepository>();
            services.AddScoped<MessageRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AuthDbContext>();

            // Cấu hình các dịch vụ xác thực trong ứng dụng ASP.NET Core để sử dụng JWT 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfig:Secret").Value);
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false
                };
            });
            services.Configure<StripeSetting>(configuration.GetSection("Stripe"));
            services.Configure<ChatDatabaseSettings>(configuration.GetSection("ChatDatabase"));

            return services;
        }
    }
}

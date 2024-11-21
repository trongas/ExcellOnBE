using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                // Look for any movies.
                if (context.Departments.Any())
                {
                    return;
                }
                context.Departments.AddRange
                (
                    new Department() { DepartmentName = "Sale", DepartmentDescription = "Sale Department " }

                );
                context.SaveChanges();
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class AuthSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AuthDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AuthDbContext>>()))
            {
                // Look for any movies.
                if (context.IdentityRoles.Any())
                {
                    return;
                }
                context.IdentityRoles.AddRange
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                    new IdentityRole() { Name = "Employee", ConcurrencyStamp = "2", NormalizedName = "Employee" },
                    new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" }
                );
                context.SaveChanges();
                // Seed a user with Admin role
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var adminUser = new IdentityUser
                {
                    UserName = "admin@gmail.com", // Set the desired username
                    Email = "admin@gmail.com",    // Set the desired email
                };

                var result = userManager.CreateAsync(adminUser, "Minh@1234").Result; // Set the desired password

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait(); // Add the user to the "Admin" role
                }

                context.SaveChanges();
            }
        }
    }
}

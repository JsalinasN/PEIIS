using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NimBaseNetCore20.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimBaseNetCore20.Data
{
    public class DbInitializer
    {
        //public static void Initialize(ApplicationDbContext context)
        public async static void Initialize(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            if ((context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                await InitializeAdministrator(applicationBuilder, context);
        }

        private async static Task InitializeAdministrator(IApplicationBuilder applicationBuilder, ApplicationDbContext context)
        {
            var logger = applicationBuilder.ApplicationServices.GetRequiredService<ILogger<DbInitializer>>();
            var roleName = "ADMINISTRATOR";

            if (!context.Roles.Any(m => m.Name == roleName))
            {
                var roleManager = applicationBuilder.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                    logger.LogInformation("Role " + roleName + " created");
            }

            var userName = "admin@nimtech.com.mx";
           if (!context.Users.Any(m => m.UserName == userName))
            {
                var userManager = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = new ApplicationUser { UserName = userName, Email = userName };

                var result = await userManager.CreateAsync(user, "Password1.");
                if (result.Succeeded)
                {
                    logger.LogInformation("Desault user " + user.UserName + " created");
                    var assignRoleResult = await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), roleName);
                    if (assignRoleResult.Succeeded)
                        logger.LogInformation("Role " + roleName + " assigned to user " + user.UserName);
                }
            }
        }
    }
}

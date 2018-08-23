using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PEIIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEIIS.Data
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
            List<string> roleNames = new List<string>()
            {
                "ADMINISTRATOR",
                "MEDICO",
                "PACIENTE"
            };

            foreach (var rol in roleNames)
                if (!context.Roles.Any(m => m.Name == rol))
                {
                    var roleManager = applicationBuilder.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
                    var result = await roleManager.CreateAsync(new IdentityRole(rol));
                    if (result.Succeeded)
                        logger.LogInformation("Role " + rol + " created");
                }

            var userName = "admin@switchstudios.com.mx";
            if (!context.Users.Any(m => m.UserName == userName))
            {
                var userManager = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = new ApplicationUser { UserName = userName, Email = userName };

                var result = await userManager.CreateAsync(user, "Password1.");
                if (result.Succeeded)
                {
                    logger.LogInformation("Default user " + user.UserName + " created");
                    var assignRoleResult = await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), "ADMINISTRATOR");
                    if (assignRoleResult.Succeeded)
                        logger.LogInformation("Role ADMINISTRATOR assigned to user " + user.UserName);
                }
            }

            userName = "medico@switchstudios.com.mx";
            if (!context.Users.Any(m => m.UserName == userName))
            {
                var userManager = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = new ApplicationUser { UserName = userName, Email = userName };

                var result = await userManager.CreateAsync(user, "Password1.");
                if (result.Succeeded)
                {
                    logger.LogInformation("Default user " + user.UserName + " created");
                    var assignRoleResult = await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), "MEDICO");
                    if (assignRoleResult.Succeeded)
                        logger.LogInformation("Role Medico assigned to user " + user.UserName);
                }
            }
            userName = "paciente@switchstudios.com.mx";
            if (!context.Users.Any(m => m.UserName == userName))
            {
                var userManager = applicationBuilder.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = new ApplicationUser { UserName = userName, Email = userName };

                var result = await userManager.CreateAsync(user, "Password1.");
                if (result.Succeeded)
                {
                    logger.LogInformation("Default user " + user.UserName + " created");
                    var assignRoleResult = await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), "PACIENTE");
                    if (assignRoleResult.Succeeded)
                        logger.LogInformation("Role Paciente assigned to user " + user.UserName);
                }
            }





        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApp.Models
{
   public class SeedData
   {
      private const string adminRole = "admin";
      private const string username = "test";
      private const string password = "123Abc!";

      private string[] defaultRoles = new string[] { adminRole, "freeAccount" };

      private readonly RoleManager<IdentityRole> roleManager;
      private readonly UserManager<AppUser> userManager;

      public SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
      {
         this.roleManager = roleManager;
         this.userManager = userManager;
      }

      public static async Task Run(IServiceProvider serviceProvider)
      {
         using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
         {
            var instance = serviceScope.ServiceProvider.GetRequiredService<SeedData>();
            await instance.Initialize();
         }
      }

      public async Task Initialize()
      {
         await SeedRoles();
         await SeedUsers();
      }

      private async Task SeedRoles()
      {
         //create the default roles if needed
         foreach (var role in defaultRoles)
         {
            if (!await roleManager.RoleExistsAsync(role))
            {
               await roleManager.CreateAsync(new IdentityRole(role));
            }
         }
      }

      private async Task SeedUsers()
      {
         var adminUsers = await userManager.GetUsersInRoleAsync(adminRole);
         if (!adminUsers.Any())
         {
            var defaultAdminUser = new AppUser()
            {
               UserName = username
            };

            var result = await userManager.CreateAsync(defaultAdminUser, password);
            await userManager.AddToRoleAsync(defaultAdminUser, adminRole);
         }
      }
   }
}
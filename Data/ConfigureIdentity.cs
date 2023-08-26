using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Data
{
    public class ConfigureIdentity
    {
        public static async Task CreateAdminUserAsync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            // Define your roles here
            string[] roleNames = { "Visitor", "Customer", "Admin" };

            foreach (var roleName in roleNames)
            {
                // if role doesn't exist, create it
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            // Define an admin user
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, "Password@1");
                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, new[] { "Admin" });
                }
            }
        }
    }
}

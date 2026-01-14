using Microsoft.AspNetCore.Identity;

namespace StorePages.Data
{
    public static class MyIdentityDataInitializer
    {
        public static async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Consumer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Consumer"));
            }
        }
        
        public static async Task SeedOneUser(UserManager<IdentityUser> userManager, string email, string password, string role = null)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
                foreach (var error in result.Errors) 
                    Console.WriteLine($"Błąd tworzenia {email}: {error.Description}");
            }
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            await SeedOneUser(userManager, "admin1@sklep", "pass", "Admin");
            await SeedOneUser(userManager, "admin2@sklep", "pass", "Admin");

            await SeedOneUser(userManager, "user1@sklep", "pass", "Consumer");
            await SeedOneUser(userManager, "user2@sklep", "pass", "Consumer");
            await SeedOneUser(userManager, "user3@sklep", "pass", "Consumer");
        }
    }
}
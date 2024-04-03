using api_opendata.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_opendata.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(DatabaseContext context, UserManager<AspNetUsers> userManager, RoleManager<AspNetRoles> roleManager)
        {
            // Ensure the database is created and apply migrations
            context.Database.EnsureCreated();

            // Check if there is any data in the database
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations == null || !pendingMigrations.Any())
            {
                await SeedRolesAsync(roleManager);
                await SeedUsersAsync(userManager);
                await SeedFunctionsAsync(context);
                await SeedDepartmentAsync(context);
            }
        }

        private static async Task SeedRolesAsync(RoleManager<AspNetRoles> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new AspNetRoles { Name = "Administrator", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Thanh tra", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Phòng Quản lý đất đai", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Phòng CNTT", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Phòng TNN, KS, KTTV", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Phòng Giá đất", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Phòng Đất đai", IsDeleted = false });
                await roleManager.CreateAsync(new AspNetRoles { Name = "Default", IsDefault = true, IsDeleted = false });
            }
        }

        private static async Task SeedUsersAsync(UserManager<AspNetUsers> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var admin = new AspNetUsers { UserName = "admin", IsDeleted = false };
                await userManager.CreateAsync(admin, "admin");
                await userManager.AddToRoleAsync(admin, "Administrator");
            }
        }

        private static async Task SeedFunctionsAsync(DatabaseContext context)
        {
            if (!await context.Functions!.AnyAsync())
            {
                context.Functions!.AddRange(
                    new Functions { PermitName = "View", PermitCode = "VIEW" },
                    new Functions { PermitName = "Create", PermitCode = "CREATE" },
                    new Functions { PermitName = "Edit", PermitCode = "EDIT" },
                    new Functions { PermitName = "Delete", PermitCode = "DELETE" });

                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedDepartmentAsync(DatabaseContext context)
        {
            if (!await context.Department!.AnyAsync())
            {
                context.Department!.AddRange(
                    new Department { DepartmentName = "Phòng Đất đai và ĐĐBĐ", IsDeleted = false },
                    new Department { DepartmentName = "Phòng Giá đất và GPMB", IsDeleted = false },
                    new Department { DepartmentName = "Phòng QL môi trường", IsDeleted = false },
                    new Department { DepartmentName = "Phòng TNN, KS, KTTV", IsDeleted = false },
                    new Department { DepartmentName = "VP Đăng ký Đất đai", IsDeleted = false },
                    new Department { DepartmentName = "Trung tâm phát triển quỹ đất", IsDeleted = false },
                    new Department { DepartmentName = "Trung tâm CNTT TNMT", IsDeleted = false },
                    new Department { DepartmentName = "Trung tâm Quan trắc TNMT", IsDeleted = false },
                    new Department { DepartmentName = "Thanh tra", IsDeleted = false }
                    );

                await context.SaveChangesAsync();
            }
        }
    }
}
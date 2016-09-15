using System.Data.Entity.Migrations;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Migrations
{
    /// <summary>
    /// Methods for inserting default data into the database.
    /// </summary>
    public static class SeedData
    {
        public static void DefaultSystemRoles(AppDbContext ctx)
        {
            ctx.Roles.AddOrUpdate(p => p.Name,
                new SystemRole() { Name = "Admin", Description = "Has access to some restricted features." },
                new SystemRole() { Name = "Kiosk", Description = "Has access public dashboards." },
                new SystemRole() { Name = "Super Admin", Description = "Has access to everything." }
            );

            ctx.SaveChanges();
        }

        public static void StrategicFitIndicatorCategories(AppDbContext ctx)
        {
            ctx.StrategicFitIndicatorCategories.AddOrUpdate(p => p.Name,
                new StrategicIndicatorCategory { Name = "T-Shirt Size Effort", Description = "Need a description..."},
                new StrategicIndicatorCategory { Name = "Regulatory", Description = "Need a description...", },
                new StrategicIndicatorCategory { Name = "Service Quality", Description = "Need a description...", },
                new StrategicIndicatorCategory { Name = "Business Growth", Description = "Need a description...", },
                new StrategicIndicatorCategory { Name = "Cost To Serve", Description = "Need a description...", },
                new StrategicIndicatorCategory { Name = "Staff", Description = "Need a description...", }
            );

            ctx.SaveChanges();
        }

        public static void TestSystemUsers(AppDbContext ctx)
        {
            ctx.Users.AddOrUpdate(p => p.UserName,
                new SystemUser() { UserName = "Homer1", FirstName = "Homer", LastName = "Simpson", Alias = "HomerAlias", EmailAddress = "test1@email.com", LoginEnabled = true },
                new SystemUser() { UserName = "Marge1", FirstName = "Marge", LastName = "Simpson", Alias = "MargeAlias", EmailAddress = "test2@email.com", LoginEnabled = false },
                new SystemUser() { UserName = "Bart1", FirstName = "Bart", LastName = "Simpson", Alias = "BartAlias", EmailAddress = "test3@email.com", LoginEnabled = true },
                new SystemUser() { UserName = "Lisa1", FirstName = "Lisa", LastName = "Simpson", Alias = "LisaAlias", EmailAddress = "test4@email.com", LoginEnabled = false },
                new SystemUser() { UserName = "Maggie1", FirstName = "Maggie", LastName = "Simpson", Alias = "MaggieAlias", EmailAddress = "test5@email.com", LoginEnabled = true }
            );

            ctx.SaveChanges();
        }
    }
}
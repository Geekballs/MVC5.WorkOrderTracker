using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Managers
{
    /// <summary>
    /// Really should be using the IUserService instead of these methods!
    /// Cant inject the IService* methods in the context pipeline for some reason!
    /// </summary>
    public class ApplicationAuthenticationManager
    {
        /// <summary>
        /// Checks if the request is enabled for system login.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsUserEnabled (string name)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Count(u => u.UserName == name && u.LoginEnabled) > 0;
                return user;
            }
        }

        /// <summary>
        /// Gets the system user by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SystemUser GetUserByName(string name)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.First(u => u.UserName == name);
                return user;
            }
        }

        /// <summary>
        /// Gets the system roles for the system user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<SytemUserRole> GetRolesForUser(Guid userId)
        {
            using (var ctx = new AppDbContext())
            {
                var userRoles = ctx.UserRoles.Include(r => r.SystemRole).Where(ur => ur.SystemUserId == userId).ToList();
                return userRoles;
            }
        }
    }
}
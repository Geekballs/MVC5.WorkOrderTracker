using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Web.Lib.Data.Contexts;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    /// <summary>
    /// Service for managing system users.
    /// </summary>
    public class SystemUserService : ISystemUserService, IDisposable
    {
        /// <summary>
        /// Declare the database connection.
        /// </summary>
        private readonly AppDbContext _ctx;

        /// <summary>
        /// Initiaite the database connection for this service.
        /// </summary>
        /// <param name="ctx"></param>
        public SystemUserService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Gets all system users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemUser> GetAllUsers()
        {
            var users = _ctx.Users.Include(t => t.SystemUserRoles).OrderBy(p => p.UserName).ToList();
            return users;
        }

        /// <summary>
        /// Gets a system user by ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SystemUser GetUserById(Guid userId)
        {
            var user = _ctx.Users.Include(t => t.SystemUserRoles).First(p => p.SystemUserId == userId);
            return user;
        }

        /// <summary>
        /// Gets system roles for this sytem user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<SytemUserRole> GetRolesForUser(Guid userId)
        {
            var userRoles = _ctx.UserRoles.Include(t => t.SystemRole).Where(p => p.SystemUserId == userId).ToList();
            return userRoles;
        }

        /// <summary>
        /// Creates a new system user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="alias"></param>
        /// <param name="emailAddress"></param>
        /// <param name="loginEnabled"></param>
        /// <param name="roles"></param>
        public void CreateUser(string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles)
        {
            var user = new SystemUser()
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Alias = alias,
                EmailAddress = emailAddress,
                LoginEnabled = loginEnabled
            };
            foreach (var i in roles)
            {
                var role = _ctx.Roles.Find(i);
                user.SystemUserRoles.Add(new SytemUserRole()
                {
                    SystemRoleId = role.SystemRoleId,
                    SystemUserId = user.SystemUserId
                });
            }
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Edits a system user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="alias"></param>
        /// <param name="emailAddress"></param>
        /// <param name="loginEnabled"></param>
        /// <param name="roles"></param>
        public void EditUser(Guid userId, string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles)
        {
            var user = _ctx.Users.First(p => p.SystemUserId == userId);
            user.UserName = userName;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Alias = alias;
            user.EmailAddress = emailAddress;
            user.LoginEnabled = loginEnabled;
            user.SystemUserRoles.Clear();
            _ctx.SaveChanges();

            foreach (var i in roles)
            {
                var role = _ctx.Roles.Find(i);
                user.SystemUserRoles.Add(new SytemUserRole()
                {
                    SystemRoleId = role.SystemRoleId,
                    SystemUserId = user.SystemUserId,
                });
            }
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Deletes a system user.
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(Guid userId)
        {
            var user = _ctx.Users.First(p => p.SystemUserId == userId);
            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Binds the save changes method.
        /// </summary>
        public void Save()
        {
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Has disposed already been called?
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Dispose of unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Suppress finalization.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
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
    public class SystemRoleService : ISystemRoleService, IDisposable
    {
        /// <summary>
        /// Declare the database connection.
        /// </summary>
        private readonly AppDbContext _ctx;

        /// <summary>
        /// Initiaite the database connection for this service.
        /// </summary>
        /// <param name="ctx"></param>
        public SystemRoleService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Gets all lsystem roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SystemRole> GetAllRoles()
        {
            var roles = _ctx.Roles.Include(t => t.SystemUserRoles).OrderBy(p => p.Name).ToList();
            return roles;
        }

        /// <summary>
        /// Gets a system role by ID.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SystemRole GetRoleById(Guid roleId)
        {
            var role = _ctx.Roles.Include(t => t.SystemUserRoles).First(p => p.SystemRoleId == roleId);
            return role;
        }

        /// <summary>
        /// Gets all system users which beling to this system role.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IEnumerable<SytemUserRole> GetUsersInRole(Guid roleId)
        {
            var roleUsers = _ctx.UserRoles.Include(t => t.SystemUser).Where(p => p.SystemRoleId == roleId).OrderBy(p => p.SystemUser.UserName).ToList();
            return roleUsers;
        }

        /// <summary>
        /// Creates a new system role.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void CreateRole(string name, string description)
        {
            var role = new SystemRole()
            {
                Name = name,
                Description = description
            };
            _ctx.Roles.Add(role);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Edits a system role.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void EditRole(Guid roleId, string name, string description)
        {
            var role = _ctx.Roles.First(p => p.SystemRoleId == roleId);
            role.Name = name;
            role.Description = description;
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Deletes a system role.
        /// </summary>
        /// <param name="roleId"></param>
        public void DeleteRole(Guid roleId)
        {
            var role = _ctx.Roles.First(p => p.SystemRoleId == roleId);
            _ctx.Roles.Remove(role);
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
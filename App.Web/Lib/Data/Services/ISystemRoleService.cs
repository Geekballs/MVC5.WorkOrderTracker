using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    /// <summary>
    /// SystemRoleService members.
    /// </summary>
    public interface ISystemRoleService
    {
        IEnumerable<SystemRole> GetAllRoles();
        SystemRole GetRoleById(Guid roleId);
        IEnumerable<SytemUserRole> GetUsersInRole(Guid roleId);
        void CreateRole(string name, string description);
        void EditRole(Guid roleId, string name, string description);
        void DeleteRole(Guid roleId);
        void Save();
    }
}

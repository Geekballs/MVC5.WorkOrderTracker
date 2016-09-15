using System;
using System.Collections.Generic;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Services
{
    /// <summary>
    /// SystemUserService members.
    /// </summary>
    public interface ISystemUserService
    {
        IEnumerable<SystemUser> GetAllUsers();
        SystemUser GetUserById(Guid userId);
        IEnumerable<SytemUserRole> GetRolesForUser(Guid userId);
        void CreateUser(string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles);
        void EditUser(Guid userId, string userName, string firstName, string lastName, string alias, string emailAddress, bool loginEnabled, IEnumerable<Guid> roles);
        void DeleteUser(Guid userId);
        void Save();
    }
}

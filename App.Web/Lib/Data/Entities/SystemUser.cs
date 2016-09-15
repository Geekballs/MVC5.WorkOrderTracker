using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class SystemUser : BaseEntity
    {
        public SystemUser()
        {
            SystemUserRoles = new HashSet<SytemUserRole>();
        }

        #region Properties

        public Guid SystemUserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string EmailAddress { get; set; }
        public bool LoginEnabled { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<SytemUserRole> SystemUserRoles { get; set; }

        #endregion
    }
}
using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class SystemRole : BaseEntity
    {
        public SystemRole()
        {
            SystemUserRoles = new HashSet<SytemUserRole>();
        }

        #region Properties

        public Guid SystemRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<SytemUserRole> SystemUserRoles { get; set; }

        #endregion
    }
}
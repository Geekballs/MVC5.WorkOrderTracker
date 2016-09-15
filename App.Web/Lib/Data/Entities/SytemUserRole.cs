using System;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class SytemUserRole : BaseEntity
    {
        #region Properties

        public Guid SystemUserId { get; set; }
        public Guid SystemRoleId { get; set; }

        #endregion

        #region Navigation Properties

        public SystemUser SystemUser { get; set; }
        public SystemRole SystemRole { get; set; }

        #endregion
    }
}
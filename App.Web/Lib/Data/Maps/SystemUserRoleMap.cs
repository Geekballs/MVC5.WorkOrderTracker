using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class SystemUserRoleMap : EntityTypeConfiguration<SytemUserRole>
    {
        public SystemUserRoleMap()
        {
            #region Table

            ToTable(tableName: "SystemUserRole", schemaName: "Security");

            #endregion

            #region Keys

            HasKey(k => new { k.SystemUserId, k.SystemRoleId });

            #endregion

            #region Relationships

            HasRequired(r => r.SystemUser).WithMany(r => r.SystemUserRoles).HasForeignKey(r => r.SystemUserId);
            HasRequired(r => r.SystemRole).WithMany(r => r.SystemUserRoles).HasForeignKey(r => r.SystemRoleId);

            #endregion

            #region Properties

            Property(p => p.SystemUserId).IsRequired().HasColumnName("SystemUserId").HasColumnOrder(1);
            Property(p => p.SystemRoleId).IsRequired().HasColumnName("SystemRoleId").HasColumnOrder(2);

            #endregion
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class SystemRoleMap : EntityTypeConfiguration<SystemRole>
    {
        public SystemRoleMap()
        {
            #region Table

            ToTable(tableName: "SystemRole", schemaName: "Security");

            #endregion

            #region Keys

            HasKey(k => new { k.SystemRoleId });

            #endregion

            #region Relationships

            // Nothing to see here!

            #endregion

            #region Properties

            Property(p => p.SystemRoleId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("SystemRoleId").HasColumnOrder(1);
            Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(2);
            Property(p => p.Description).HasMaxLength(450).IsRequired().HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}
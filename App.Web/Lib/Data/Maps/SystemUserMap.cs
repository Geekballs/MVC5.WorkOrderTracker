using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class SystemUserMap : EntityTypeConfiguration<SystemUser>
    {
        public SystemUserMap()
        {
            #region Table

            ToTable(tableName: "SystemUser", schemaName: "Security");

            #endregion

            #region Keys

            HasKey(k => new { k.SystemUserId });

            #endregion

            #region Relationships

            // Nothing to see here!

            #endregion

            #region Properties

            Property(p => p.SystemUserId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("SystemUserId").HasColumnOrder(1);
            Property(p => p.UserName).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("UserName").HasColumnOrder(2);
            Property(p => p.FirstName).IsRequired().HasMaxLength(100).HasColumnName("FirstName").HasColumnOrder(3);
            Property(p => p.LastName).IsRequired().HasMaxLength(100).HasColumnName("LastName").HasColumnOrder(4);
            Property(p => p.Alias).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Alias", 3) { IsUnique = true } })).HasColumnName("Alias").HasColumnOrder(5);
            Property(p => p.EmailAddress).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_EmailAddress", 4) { IsUnique = true } })).HasColumnName("EmailAddress").HasColumnOrder(6);
            Property(p => p.LoginEnabled).IsRequired().HasColumnName("LoginEnabled").HasColumnOrder(7);

            #endregion
        }
    }
}
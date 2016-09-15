using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class WorkRequestMap : EntityTypeConfiguration<WorkRequest>
    {
        public WorkRequestMap()
        {
            #region Table
              
            ToTable(tableName: "WorkRequest", schemaName: "Activity");

            #endregion

            #region Keys

            HasKey(k => new { k.WorkRequestId });

            #endregion

            #region Relationships

            HasOptional(r => r.WorkItem).WithRequired(r => r.WorkRequest).WillCascadeOnDelete(true);

            #endregion

            #region Properties
              
            Property(p => p.WorkRequestId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_WorkRequestId", 1) { IsUnique = true } })).HasColumnName("WorkRequestId").HasColumnOrder(1);
            Property(p => p.Title).IsRequired().HasMaxLength(200).HasColumnName("Title").HasColumnOrder(2);
            Property(p => p.Description).IsRequired().HasMaxLength(2000).HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}
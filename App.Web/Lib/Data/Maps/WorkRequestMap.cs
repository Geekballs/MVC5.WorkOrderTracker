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
              
            HasKey(k => k.WorkRequestId);

            #endregion

            #region Relationships

            HasOptional(wi => wi.WorkItem).WithRequired(wr => wr.WorkRequest).WillCascadeOnDelete(true);

            #endregion

            #region Properties
              
            Property(wr => wr.WorkRequestId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_WorkRequestId", 1) { IsUnique = true } })).HasColumnName("WorkRequestId").HasColumnOrder(1);
            Property(wr => wr.Title).IsRequired().HasMaxLength(200).HasColumnName("Title").HasColumnOrder(2);
            Property(wr => wr.Description).IsRequired().HasMaxLength(2000).HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}
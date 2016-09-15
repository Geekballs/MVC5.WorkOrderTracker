using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class WorkItemMap : EntityTypeConfiguration<WorkItem>
    {
        public WorkItemMap()
        {
            #region Table

            ToTable(tableName: "WorkItem", schemaName: "Activity");

            #endregion

            #region Keys

            HasKey(k => new { k.WorkItemId });

            #endregion

            #region Relationships

            HasRequired(r => r.TShirtSizeEffort).WithMany(r => r.TShirtSizeEfforts).HasForeignKey(r => new { r.TShirtSizeEffortId }).WillCascadeOnDelete(false);
            HasRequired(r => r.CostToServe).WithMany(r => r.CostsToServe).HasForeignKey(r => new { r.CostToServeId }).WillCascadeOnDelete(false);
            HasRequired(r => r.Regulatory).WithMany(r => r.Regulatories).HasForeignKey(r => new { r.RegulatoryId }).WillCascadeOnDelete(false);
            HasRequired(r => r.ServiceQuality).WithMany(r => r.ServiceQualities).HasForeignKey(r => new { r.ServiceQualityId }).WillCascadeOnDelete(false);

            #endregion

            #region Properties

            Property(p => p.WorkItemId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("WorkItemId").HasColumnOrder(1);
            Property(p => p.Title).IsRequired().HasMaxLength(200).HasColumnName("Title").HasColumnOrder(2);
            Property(p => p.Description).IsRequired().HasMaxLength(2000).HasColumnName("Description").HasColumnOrder(3);
            Property(p => p.TShirtSizeEffortId).IsRequired().HasColumnName("TShirtSizeEffortId").HasColumnOrder(4);
            Property(p => p.CostToServeId).IsRequired().HasColumnName("CostToServeId").HasColumnOrder(5);
            Property(p => p.RegulatoryId).IsRequired().HasColumnName("RegulatoryId").HasColumnOrder(6);
            Property(p => p.ServiceQualityId).IsRequired().HasColumnName("ServiceQualityId").HasColumnOrder(7);


            #endregion
        }
    }
}
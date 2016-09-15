using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class StrategicIndicatorMap : EntityTypeConfiguration<StrategicIndicator>
    {
        public StrategicIndicatorMap()
        {
            #region Table

            ToTable(tableName: "StrategicIndicator", schemaName: "Metric");

            #endregion

            #region Keys

            HasKey(k => new { k.StrategicIndicatorId });

            #endregion

            #region Relationships

            HasRequired(r => r.StrategicIndicatorCategory);

            #endregion

            #region Properties

            Property(p => p.StrategicIndicatorId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("StrategicIndicatorId").HasColumnOrder(1);
            Property(p => p.StrategicIndicatorCategoryId).IsRequired().HasColumnName("StrategicIndicatorCategoryId").HasColumnOrder(2);
            Property(p => p.Name).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(3);
            Property(p => p.Description).IsRequired().HasMaxLength(450).HasColumnName("Description").HasColumnOrder(4);

            #endregion
        }
    }
}
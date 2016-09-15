using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class StrategicFitIndicatorMap : EntityTypeConfiguration<StrategicFitIndicator>
    {
        public StrategicFitIndicatorMap()
        {
            #region Table

            ToTable(tableName: "StrategicFitIndicator", schemaName: "Metric");

            #endregion

            #region Keys

            HasKey(k => new { k.StrategicFitIndicatorId });

            #endregion

            #region Relationships

            HasRequired(r => r.StrategicFitIndicatorCategory);

            #endregion

            #region Properties

            Property(p => p.StrategicFitIndicatorId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("StrategicFitIndicatorId").HasColumnOrder(1);
            Property(p => p.StrategicFitIndicatorCategoryId).IsRequired().HasColumnName("StrategicIndicatorCategoryId").HasColumnOrder(2);
            Property(p => p.Name).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(3);
            Property(p => p.Description).IsRequired().HasMaxLength(450).HasColumnName("Description").HasColumnOrder(4);

            #endregion
        }
    }
}
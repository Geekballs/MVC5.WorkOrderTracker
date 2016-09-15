using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class StrategicFitIndicatorCategoryMap : EntityTypeConfiguration<StrategicFitIndicatorCategory>
    {
        public StrategicFitIndicatorCategoryMap()
        {
            #region Table 

            ToTable(tableName: "StrategicFitIndicatorCategory", schemaName: "Metric");

            #endregion

            #region Keys
              
            HasKey(k => k.StrategicFitIndicatorCategoryId);

            #endregion

            #region Relationships

            HasMany(r => r.StrategicFitIndicators).WithRequired(r => r.StrategicFitIndicatorCategory).WillCascadeOnDelete(false);

            #endregion

            #region Properties

            Property(p => p.StrategicFitIndicatorCategoryId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("StrategicFitIndicatorCategoryId").HasColumnOrder(1);
            Property(p => p.Name).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(2);
            Property(p => p.Description).IsRequired().HasMaxLength(450).HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}
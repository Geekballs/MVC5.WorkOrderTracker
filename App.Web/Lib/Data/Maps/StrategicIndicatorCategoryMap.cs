using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    /// <summary>
    /// Entity mapping properties.
    /// </summary>
    public class StrategicIndicatorCategoryMap : EntityTypeConfiguration<StrategicIndicatorCategory>
    {
        public StrategicIndicatorCategoryMap()
        {
            #region Table 

            ToTable(tableName: "StrategicIndicatorCategory", schemaName: "Metric");

            #endregion

            #region Keys
              
            HasKey(k => k.StrategicIndicatorCategoryId);

            #endregion

            #region Relationships

            HasMany(r => r.StrategicIndicators).WithRequired(r => r.StrategicIndicatorCategory).WillCascadeOnDelete(false);

            #endregion

            #region Properties

            Property(p => p.StrategicIndicatorCategoryId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Id", 1) { IsUnique = true } })).HasColumnName("StrategicIndicatorCategoryId").HasColumnOrder(1);
            Property(p => p.Name).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(2);
            Property(p => p.Description).IsRequired().HasMaxLength(450).HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}
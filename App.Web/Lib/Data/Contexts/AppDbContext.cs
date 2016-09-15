using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Contexts
{
    /// <summary>
    /// Database configuration properties.
    /// </summary>
    public class AppDbContext : DbContext
    {
        #region Database Connections

        public AppDbContext()
            : base("name=AppConnection")
        {
        }

        #endregion

        #region Database Build 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => !string.IsNullOrEmpty(type.Namespace))
                    .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Model Configuration

        public IDbSet<SystemUser> Users { get; set; }
        public IDbSet<SystemRole> Roles { get; set; }
        public IDbSet<SytemUserRole> UserRoles { get; set; }
        public IDbSet<WorkRequest> WorkRequests { get; set; }
        public IDbSet<WorkItem> WorkItems { get; set; }
        public IDbSet<StrategicIndicator> StrategicFitIndicators { get; set; }
        public IDbSet<StrategicIndicatorCategory> StrategicFitIndicatorCategories { get; set; }

        #endregion
    }
}

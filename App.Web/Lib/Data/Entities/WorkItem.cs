using System;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class WorkItem : BaseEntity
    {
        #region Properties

        public Guid WorkItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CostToServeId { get; set; }
        public Guid TShirtSizeEffortId { get; set; }
        public Guid RegulatoryId { get; set; }
        public Guid ServiceQualityId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual WorkRequest WorkRequest { get; set; }
        public virtual StrategicIndicator TShirtSizeEffort { get; set; }
        public virtual StrategicIndicator CostToServe { get; set; }
        public virtual StrategicIndicator Regulatory { get; set; }
        public virtual StrategicIndicator ServiceQuality { get; set; }

        #endregion
    }
}
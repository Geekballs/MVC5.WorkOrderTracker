using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class StrategicIndicator
    {
        #region Properties

        public Guid StrategicIndicatorId { get; set; }
        public Guid StrategicIndicatorCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual StrategicIndicatorCategory StrategicIndicatorCategory { get; set; }

        public virtual ICollection<WorkItem> TShirtSizeEfforts { get; set; }
        public virtual ICollection<WorkItem> CostsToServe { get; set; }
        public virtual ICollection<WorkItem> Regulatories { get; set; }
        public virtual ICollection<WorkItem> ServiceQualities { get; set; }

        #endregion
    }
}

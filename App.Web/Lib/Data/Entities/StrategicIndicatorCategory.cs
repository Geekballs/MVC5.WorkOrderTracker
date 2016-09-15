using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class StrategicIndicatorCategory
    {
        #region Properties

        public Guid StrategicIndicatorCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<StrategicIndicator> StrategicIndicators { get; set; }

        #endregion
    }
}

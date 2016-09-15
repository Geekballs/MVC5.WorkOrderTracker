using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class StrategicFitIndicatorCategory
    {
        #region Properties

        public Guid StrategicFitIndicatorCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<StrategicFitIndicator> StrategicFitIndicators { get; set; }

        #endregion
    }
}

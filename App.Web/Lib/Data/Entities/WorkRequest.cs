using System;

namespace App.Web.Lib.Data.Entities
{
    /// <summary>
    /// Entity properties.
    /// </summary>
    public class WorkRequest : BaseEntity
    {
        #region Properties

        public Guid WorkRequestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual WorkItem WorkItem { get; set; }

        #endregion
    }
}
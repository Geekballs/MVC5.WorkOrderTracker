using System;

namespace App.Web.Lib.Models
{
    /// <summary>
    /// Reusable component for injecting checkboxes into a view.
    /// </summary>
    public class CheckBoxListItem
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
    }
}
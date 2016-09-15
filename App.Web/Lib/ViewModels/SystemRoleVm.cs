using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Lib.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemRoleVm
    {
        /// <summary>
        /// ...\App.Web\Views\Role\Index.cshtml
        /// </summary>
        public class Index
        {
            public Guid SystemRoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
            public int SystemRoleUserCount { get; set; }
        }

        /// <summary>
        /// ...\App.Web\Views\Role\Detail.cshtml
        /// </summary>
        public class Detail
        {
            [DisplayName("ID")]
            public Guid SystemRoleId { get; set; }

            [DisplayName("Name")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            public string RoleDescription { get; set; }

            [DisplayName("Role Users")]
            public List<RoleUsersDetail> RoleUsersDetail { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class RoleUsersDetail
        {
            public Guid SystemUserId { get; set; }
            public string UserName { get; set; }
        }

        /// <summary>
        /// ...\App.Web\Views\Role\Create.cshtml
        /// </summary>
        public class Create
        {
            [DisplayName("Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(450, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleDescription { get; set; }
        }

        /// <summary>
        /// ...\App.Web\Views\Role\Edit.cshtml
        /// </summary>
        public class Edit
        {
            public Guid SystemRoleId { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(450, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleDescription { get; set; }
        }

        /// <summary>
        /// ...\App.Web\Views\Role\Delete.cshtml
        /// </summary>
        public class Delete
        {
            public Guid SystemRoleId { get; set; }

            [DisplayName("Name")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            public string RoleDescription { get; set; }
        }
    }
}
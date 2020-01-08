using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotNET.Web.Host.Model
{
    public class UserEditModel : GoBackUrlModel
    {
        public long Id { get; set; }

        [Display(Name = "帐号")]
        [Required(ErrorMessage = "{0}必填")]
        public string Account { get; set; }

        [Display(Name = "角色")]
        [Required(ErrorMessage = "{0}必填")]
        public long RoleId { get; set; }

        public List<SelectListItem> Rolelist { get; set; } = new List<SelectListItem>();

        [Display(Name = "头像")]
        public string Avatar { get; set; }

        [Display(Name = "手机号")]
        [StringLength(12, ErrorMessage = "{0}最大12位字符")]
        public string Tel { get; set; }

        [Display(Name = "名称")]
        [StringLength(50, ErrorMessage = "{0}最大50位字符")]
        public string RealName { get; set; }

        [Display(Name = "允许登录")]
        [Required(ErrorMessage = "{0}必填")]
        public int State { get; set; }

        [Display(Name = "类型")]
        [Required(ErrorMessage = "{0}必填")]
        public bool IsSys { get; set; }

        [Display(Name = "部门")]
        [Required(ErrorMessage = "{0}必填")]
        public long? DepartmentId { get; set; }

        public List<SelectListItem> deptlist { get; set; } = new List<SelectListItem>();
    }

    /// <summary>
    ///
    /// </summary>
    public class UserCreateModel : UserEditModel
    {
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}必填")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Web.Host.Web.Model
{
    public class demock
    {
        [UIHint("kindeditor")]
        public virtual string rules { get; set; } = "<p>与一月又一月一月又一月一月又一月</p>";

        [UIHint("kindeditor")]
        public virtual string Text { get; set; }
    }

    /// <summary>
    /// 登录
    /// </summary>
    public class LoginModel
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePasswordModel
    {
        [Display(Name = "原密码")]
        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(50, ErrorMessage = "最多{1}字符")]
        public string Password { get; set; }

        [Display(Name = "新密码")]
        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(50, ErrorMessage = "最多{1}字符")]
        public string NewPassword { get; set; }

        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "{0} 必填")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}
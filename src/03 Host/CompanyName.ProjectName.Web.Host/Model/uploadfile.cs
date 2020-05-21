using Microsoft.AspNetCore.Http;

namespace CompanyName.ProjectName.Web.Host.Web.Model
{
    /// <summary>
    /// 上传图片
    /// </summary>
    public class uploadfile
    {
        //  [Required]

        //   [Display(Name = "身份证附件")]

        // [FileExtensions(Extensions = ".jpg", ErrorMessage = "图片格式错误")]

        public IFormFile ShareImg { get; set; }
    }
}
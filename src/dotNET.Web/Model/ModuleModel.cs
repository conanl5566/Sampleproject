using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace dotNET.Web.Host.Model
{
    public class ModuleModel
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        [Display(Name = "位置")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string EnCode { get; set; } = "m";

        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string FullName { get; set; }

        [Display(Name = "图标")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string Icon { get; set; }

        [Display(Name = "连接")]
        [StringLength(100, ErrorMessage = "{0}最多{1}字符")]
        public string UrlAddress { get; set; }

        [Display(Name = "目标")]
        [Required(ErrorMessage = "{0}必填")]
        public string Target { get; set; }

        [Display(Name = "菜单")]
        public bool? IsMenu { get; set; }

        [Display(Name = "展开")]
        public bool? IsExpand { get; set; }

        [Display(Name = "公共")]
        public bool? IsPublic { get; set; }

        [Display(Name = "是否有效")]
        public bool? IsEnabled { get; set; }

        [Display(Name = "目标")]
        [Required(ErrorMessage = "{0}必填")]
        public int? SortCode { get; set; }

        [Display(Name = "描述")]
        [StringLength(200, ErrorMessage = "{0}最多{1}字符")]
        public string Description { get; set; }
    }

    public class ModuleButtonModel
    {
        public long Id { get; set; }

        [Required]
        public long ParentId { get; set; }

        [Required]
        public long ModuleId { get; set; }

        [Display(Name = "位置")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string EnCode { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string FullName { get; set; }

        [Display(Name = "图标")]
        [StringLength(50, ErrorMessage = "{0}最多{1}字符")]
        public string Icon { get; set; }

        [Display(Name = "连接")]
        [StringLength(100, ErrorMessage = "{0}最多{1}字符")]
        public string UrlAddress { get; set; }

        [Display(Name = "位置")]
        public int? Location { get; set; }

        [Display(Name = "是否有效")]
        public bool? IsEnabled { get; set; }

        [Display(Name = "公共")]
        public bool? IsPublic { get; set; }

        [Display(Name = "排序")]
        [Required(ErrorMessage = "{0}必填")]
        public int? SortCode { get; set; }

        [Display(Name = "描述")]
        [StringLength(200, ErrorMessage = "{0}最多{1}字符")]
        public string Description { get; set; }
    }
}

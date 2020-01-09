using dotNET.ICommonServer;
using dotNET.CommonServer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotNET.Web.Host.Model
{
    public class DepartmentModel : GoBackUrlModel
    {
        public long Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>

        [Display(Name = "编号")]
        [Required(ErrorMessage = "{0}必填")]
        public string Code { get; set; }

        /// <summary>
        /// 部门负责人
        /// </summary>
        [Display(Name = "负责人")]
        public string Manager { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string ContactNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remarks { get; set; }

        /// <summary>
        /// 父级部门ID
        /// </summary>
        [Display(Name = "父级部门ID")]
        public long ParentId { get; set; }

        public List<SelectListItem> pids { get; set; } = new List<SelectListItem>();

        ///// <summary>
        ///// 代理商Id

        ///// </summary>
        //[Display(Name = "代理商Id")]
        //public long AgentId { get; set; }

        //[Display(Name = "创建时间")]
        //public DateTime? CreatorTime { get; set; }
        //[Display(Name = "创建人")]
        //public long? CreatorUserId { get; set; }
    }

    public class ItemsDatalistmodel
    {
        public List<ItemsData> ItemsDatalist { get; set; }
    }

    public class AreaListlistmodel
    {
        public List<AreaList> AreaListlist { get; set; }
    }

    public class Departmentlistmodel
    {
        public List<Department> Departmentlist { get; set; }
    }

    public class ItemsDataModel : GoBackUrlModel
    {
        public long Id { get; set; }

        public List<SelectListItem> pids { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "{0}最大50位字符")]
        public string Name { get; set; }

        [Display(Name = "是否生效")]
        public bool IsEnabled { get; set; } = true;

        [Display(Name = "排序")]
        [Required(ErrorMessage = "{0}必填")]
        public int? SortCode { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(100, ErrorMessage = "{0}最大100位字符")]
        public string Remarks { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Display(Name = "父级部门ID")]
        public long ParentId { get; set; }

        //[Display(Name = "创建时间")]
        //public DateTime? CreatorTime { get; set; }

        //[Display(Name = "创建人")]
        //public long? CreatorUserId { get; set; }
    }
}
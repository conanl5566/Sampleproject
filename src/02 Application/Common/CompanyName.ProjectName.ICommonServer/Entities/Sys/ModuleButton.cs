/**************************************************************************
 * 作者：X
 * 日期：2017.01.18
 * 描述：
 * 修改记录：
 * ***********************************************************************/

using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.Enum;
using System;

namespace CompanyName.ProjectName.ICommonServer
{
    public class ModuleButton : Entity, IEntity
    {
        public long ModuleId { get; set; }
        public long ParentId { get; set; }
        public string FullName { get; set; }
        public string Icon { get; set; }
        public int? Location { get; set; }
        public string UrlAddress { get; set; }
        public bool? Split { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsEnabled { get; set; }
        public bool? AllowEdit { get; set; }
        public bool? AllowDelete { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }

        public DateTime CreatorTime { get; set; }
    }
}
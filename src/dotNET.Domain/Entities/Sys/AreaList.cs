/**************************************************************************
 * 作者：X   
 * 日期：2017.01.18   
 * 描述：
 * 修改记录：    
 * ***********************************************************************/

using System.ComponentModel.DataAnnotations.Schema;

namespace dotNET.Domain.Entities.Sys
{
    /// <summary>
    /// 省市区
    /// </summary>
    public class AreaList : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Postcode { get; set; }

       /// <summary>
       /// 上一级id
       /// </summary>

        public long  ParentID { get; set; }

        /// <summary>
        /// 等级？
        /// </summary>
        public int AreaGrade { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortID { get; set; }

        /// <summary>
        /// 区域代码
        /// </summary>
        public string AreaCode { get; set; }


        /// <summary> 
        /// 区域类型    1  省  2  市  3  区
        /// </summary>
        public int AreaType { get; set; }


        /// <summary>
        /// 关键字
        /// </summary>
        public string AreaKeys { get; set; }

    }
}

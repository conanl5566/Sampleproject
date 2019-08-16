using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET.Dto
{
    public class OperateLogOption : Option
    {
        /// <summary>
        /// 所属代理
        /// </summary>
        public long AgentId { get; set; }

        /// <summary>
        /// 操作对象Id
        /// </summary>
        public long? ObjectId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDateTime { get; set; }
    }




    public class OperateLogDto 
    {

        public long Id { get; set; }
        /// <summary>
        /// 所属代理
        /// </summary>
        public long AgentId { get; set; }
        /// <summary>
        /// 操作者Id
        /// </summary>
        public long OperatorId { get; set; }

        public string OperatorName{ get; set; }

        /// <summary>
        /// 操作都类型  会员，代理商，Saas后台
        /// </summary>
        public string OperatorType { get; set; }

        /// <summary>
        /// 操作对象Id
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        /// 操作对象表名
        /// </summary>
        public string ObjectTable { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }


    
    }


}

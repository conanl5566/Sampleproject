/**************************************************************************
 * 作者：X
 * 日期：2017.01.19
 * 描述：Entity
 * 修改记录：
 * ***********************************************************************/

using CompanyName.ProjectName.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyName.ProjectName.Core
{
    public abstract class Entity
    {
        private static IdWorker _idWorker = null;

        private static DateTime _sdt = DateTime.Today;

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Required]
        public virtual long Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="datacenterId"></param>
        /// <returns></returns>
        protected long CreateId(EntityEnum datacenterId)
        {
            if (_idWorker == null)
            {
                _idWorker = new IdWorker(GetWorkerId(), (int)datacenterId);
                _sdt = DateTime.Today;
            }
            else if (_sdt < DateTime.Now.AddDays(-1))
            {
                _idWorker = new IdWorker(GetWorkerId(), (int)datacenterId);
                _sdt = DateTime.Today;
            }

            return _idWorker.NextId();
        }

        private long GetWorkerId()
        {
            var config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
            long workerId = config.GetValue<long>("Data:WorkerId");

            if (workerId > 31)
                return 31;
            if (workerId < 0)
                return 0;

            return workerId;
        }
    }
}
using System.ComponentModel;

namespace dotNET.Enum
{
    /// <summary>
    /// 当前环境配置
    /// </summary>
    public enum CurrentEnvironmentEnum
    {
        /// <summary>
        /// 开发环境
        /// </summary>
        [Description("开发环境")]
        Dev = 1,

        /// <summary>
        /// 测试环境
        /// </summary>
        [Description("测试环境")]
        Test = 2,

        /// <summary>
        /// 仿真环境
        /// </summary>
        [Description("仿真环境")]
        TestPro = 3,

        /// <summary>
        /// 生产环境
        /// </summary>
        [Description("生产环境")]
        Pro = 4
    }
}
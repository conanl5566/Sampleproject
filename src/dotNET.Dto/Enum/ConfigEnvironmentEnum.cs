using System.ComponentModel;

namespace dotNET.Dto.Enum
{

    public enum ConfigEnvironmentEnum
    {
        /// <summary>
        /// 公共环境
        /// </summary>
        [Description("Common")]
        Common = 0,
        /// <summary>
        /// 测试环境
        /// </summary>
        [Description("Test")]
        Test = 1,
        /// <summary>
        /// 生产环境
        /// </summary>
        [Description("Pro")]
        Pro = 2
    }
}

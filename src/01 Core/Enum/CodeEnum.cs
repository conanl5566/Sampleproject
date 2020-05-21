using System.ComponentModel;

namespace CompanyName.ProjectName.Enum
{
    /// <summary>
    ///
    /// </summary>
    public enum CodeEnum
    {
        [Description("操作成功")]
        Ok = 0,

        [Description("操作失败")]
        Fail = -1,
    }
}
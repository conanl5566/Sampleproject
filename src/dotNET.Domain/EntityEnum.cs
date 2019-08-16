using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET.Domain
{
    public enum EntityEnum
    {
        //sys
        Other = 1,
        Module,
        ModuleButton,
        RoleAuthorize,
        Role,
        APILog,
        ErrorLog,
        OperateLog,
        PaymentSetting,

        //agent
        User,
        Agent,
        Member,
        LoginLog,
        VisitDomain,
        Department,
        PaySetting,
    }
}

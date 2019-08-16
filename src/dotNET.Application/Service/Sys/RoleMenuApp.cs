#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AC.Utility;
using AC.Utility.Security;
using AC.Data;
using AC.Repository;
using AC.Domain.Entities;
using AC.Application.Infrastructure;
using AC.Dto;
using AC.Utility.Cache;
#endregion

namespace AC.Application.App
{
    public class RoleMenuApp : App, IRoleMenuApp
    {
        IDbContext context;
        RoleMenuRep _RoleMenuRep;
        RoleRep _RoleRep;
        IOperateLogApp _operateLogApp;
        ICacheService _cache;
        string prefix = "RoleMenu";

        public RoleMenuApp(IDbContext dbContext, IOperateLogApp operateLogApp) : base(dbContext)
        {
            context = dbContext;
            _RoleMenuRep = new RoleMenuRep(dbContext);
            _RoleRep = new RoleRep(dbContext);
            _operateLogApp = operateLogApp;
            _cache = CacheFactory.Cache();
        }

        #region 获取角色权限
        public async Task<List<RoleMenu>> GetlistAsync(long Id)
        {
            var list = await _cache.GetAsync<List<RoleMenu>>(Id.ToString(), prefix);
            if (list == null)
            {
                var vd = await _RoleMenuRep.QueryAsync<RoleMenu>("select  *  from `rolemenus` where `RoleId`=@RoleId", new { RoleId = Id });
                if (vd != null)
                {
                    await _cache.AddAsync(Id.ToString(), vd, prefix);
                    return vd.ToList();
                }
                else
                {
                    return new List<RoleMenu>();
                }
            }
            return list;
        }
        #endregion

        #region 保存角色权限
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionCodes"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public async Task<R> AssignPermissionFor(long roleId, string[] permissionCodes, CurrentUser CurrentUser)
        //{
        //    //   Convert.ToInt32("re");
        //    //  string logstr = "";
        //    //try
        //    //{
        //    using (IDbContext context22 = new DbContext(Zconfig.GetMyCat()))
        //    {

        //        context22.Open();
        //        context22.BeginTransaction();

        //        IRoleApp _RoleApp2 = new RoleApp(context22);
        //        IRoleMenuApp _RoleMenuApp2 = new RoleMenuApp(context22);

        //        var rl = await _RoleMenuApp2.GetlistAsync(roleId);
        //        List<RoleMenu> rolePermissions = rl.ToList();
        //        var listadd = new List<string>();
        //        var listdelete = new List<string>();
        //        //using (context)
        //        //{


        //        #region 验证
        //        var role = await _RoleApp2.GetAsync(roleId);
        //        if (role == null)
        //            return R.Err("角色不存在");
        //        #endregion

        //        #region 权限代码
        //        #region MyRegion

        //        if (permissionCodes != null && permissionCodes.Length > 0)
        //        {
        //            //  logstr += "角色权限添加:";

        //            var dellist = rolePermissions.Where(o => !permissionCodes.Contains(o.MenuId)).Select(o => o.MenuId).ToList();


        //            foreach (string s in permissionCodes)
        //            {

        //                var obj = rolePermissions.Where(o => o.MenuId == s).FirstOrDefault();
        //                if (obj == null)
        //                {
        //                    await new RoleMenuRep(context22).InsertAsync(new RoleMenu { Id = Rnd.RndId(), RoleId = roleId, MenuId = s, CreatorTime = System.DateTime.Now });
        //                    //  logstr += s + ";";

        //                    listadd.Add(s);
        //                    //日志
        //                    //   OperateLogBLL.GetInstance().InsertLog(uinfo, "角色权限添加", objRolePermission);
        //                }
        //            }


        //            if (dellist != null && dellist.Count > 0)
        //                foreach (var p in dellist)
        //                {
        //                    listdelete.Add(p);
        //                    await new RoleMenuRep(context22).DeleteListAsync("where `MenuId`=@MenuId  and `RoleId`=@RoleId  ", new RoleMenu { MenuId = p, RoleId = roleId });
        //                }

        //        }
        //        #endregion

        //        else
        //        {
        //            await new RoleMenuRep(context22).DeleteListAsync("where  `RoleId`=@RoleId  ", new RoleMenu { RoleId = roleId });

        //            //  logstr += "删除全部角色权限:";

        //        }
        //        #endregion


        //        //}

        //        #region log
        //        if (listadd.Count > 0 || listdelete.Count > 0)
        //        {
        //            string logstr = "";
        //            if (listadd.Count > 0)
        //            {
        //                logstr += "添加权限：" + string.Join(",", listadd);
        //            }
        //            if (listdelete.Count > 0)
        //            {
        //                logstr += "删除权限：" + string.Join(",", listdelete);
        //            }
        //            await new OperateLogRep(context22).InsertCusAsync<RoleMenu, long>(CurrentUser.UserId, "角色权限", "Saas后台", 0, logstr, CurrentUser.IP);

        //        }
        //        else
        //        {
        //            if (rolePermissions != null && rolePermissions.Count > 0)
        //            {
        //                var listdeall = rolePermissions.Select(o => o.MenuId).ToList();

        //                string logstr = "";

        //                logstr += "删除权限：" + string.Join(",", listdeall);


        //                await new OperateLogRep(context22).InsertCusAsync<RoleMenu, long>(CurrentUser.UserId, "角色权限", "Saas后台", 0, logstr, CurrentUser.IP);

        //            }
        //        }
        //        #endregion


        //        //}
        //        //catch (Exception ex)
        //        //{

        //        //    NLogger.Error(ex, "角色权限保存");
        //        //    return R.Err("保存失败");
        //        //}

        //        context22.Commit();
        //        context22.Close();

        //    }
        //    return R.Suc();
        //}

        #endregion

        #region 保存角色权限
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionCodes"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<R> AssignPermissionFor(long roleId, string[] permissionCodes, CurrentUser CurrentUser)
        {
            //  string logstr = "";

            #region 验证
            var role = await _RoleRep.GetAsync(roleId);
            if (role == null)
                return R.Err("角色不存在");
            #endregion

            var rl = await GetlistAsync(roleId);
            List<RoleMenu> rolePermissions = rl.ToList();
            var listadd = new List<string>();
            var listdelete = new List<string>();


            await _cache.RemoveAsync(roleId.ToString(),prefix);
            try
            {
             
          
            context.Open();
            context.BeginTransaction();
             

            #region 权限代码
                #region MyRegion

                if (permissionCodes != null && permissionCodes.Length > 0)
            {
                //  logstr += "角色权限添加:";

                var dellist = rolePermissions.Where(o => !permissionCodes.Contains(o.MenuId)).Select(o => o.MenuId).ToList();


                    foreach (string s in permissionCodes)
                    {

                        var obj = rolePermissions.Where(o => o.MenuId == s).FirstOrDefault();
                        if (obj == null)
                        {
                            var rotemenu = new RoleMenu { RoleId = roleId, MenuId = s, CreatorTime = System.DateTime.Now };
                            rotemenu.Id = rotemenu.CreateId();
                            await _RoleMenuRep.InsertAsync(rotemenu);
                            //  logstr += s + ";";

                            listadd.Add(s);
                            //日志
                            //   OperateLogBLL.GetInstance().InsertLog(uinfo, "角色权限添加", objRolePermission);
                        }
                    }


                if (dellist != null && dellist.Count > 0)
                    foreach (var p in dellist)
                    {
                        listdelete.Add(p);
                        await _RoleMenuRep.DeleteListAsync("where `MenuId`=@MenuId  and `RoleId`=@RoleId  ", new RoleMenu { MenuId = p, RoleId = roleId });
                    }

            }
            #endregion

            else
            {
                await _RoleMenuRep.DeleteListAsync("where  `RoleId`=@RoleId  ", new RoleMenu { RoleId = roleId });

                //  logstr += "删除全部角色权限:";

            }
            #endregion

          
         
          

            #region log
            if (listadd.Count > 0 || listdelete.Count > 0)
            {
                string logstr = "";
                if (listadd.Count > 0)
                {
                    logstr += "添加权限：" + string.Join(",", listadd);
                }
                if (listdelete.Count > 0)
                {
                    logstr += "删除权限：" + string.Join(",", listdelete);
                }
                await _operateLogApp.InsertCusAsync(CurrentUser, "角色权限", logstr, roleId, "RoleMenus");

            }
            else
            {
                if (rolePermissions != null && rolePermissions.Count > 0)
                {
                    var listdeall = rolePermissions.Select(o => o.MenuId).ToList();

                    string logstr = "";

                    logstr += "删除权限：" + string.Join(",", listdeall);


                    await _operateLogApp.InsertCusAsync<RoleMenu>(CurrentUser, "角色权限", logstr);

                }
            }
                #endregion
           context.Commit();

            }
            catch (Exception ex)
            {

             //   NLogger.Error(ex, "角色权限保存");
                return R.Err("保存失败");
            }
            return R.Suc();
        }

        #endregion

    }
}
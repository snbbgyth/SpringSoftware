﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpringSoftware.Core.QueueDAL;
using SpringSoftware.Web.Areas.Admin.Models;

namespace SpringSoftware.Web.DAL.Manage
{
    public class UserRoleManage
    {
        private static UserManager<ApplicationUser> _userManager { get; set; }
        private static RoleManager<IdentityRole> _roleManager { get; set; }

        static UserRoleManage()
        {
            var context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        public static     IEnumerable<ApplicationUser>  QueryLast(int count)
        {
            try
            {
                return   _userManager.Users.Take(count).ToList();
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(typeof(CommentManage), MethodBase.GetCurrentMethod().Name, ex);
                return new List<ApplicationUser>();
            }
        }
    }
}
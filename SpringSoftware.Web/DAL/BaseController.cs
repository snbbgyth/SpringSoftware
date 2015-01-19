using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.Model;

namespace SpringSoftware.Web.DAL
{
    [MyAuthorize]
    public class BaseController:Controller
    {
        public T InitInsert<T>(T entity) where T : BaseTable
        {
            entity.Creater = User.Identity.Name;
            entity.LastModifier = User.Identity.Name;
            entity.CreateDate = DateTime.Now;
            entity.LastModifyDate = DateTime.Now;
            return entity;
        }

        public T InitModify<T>(T entity) where T : BaseTable
        {
            entity.LastModifier = User.Identity.Name;
            entity.LastModifyDate = DateTime.Now;
            return entity;
        }
    }
}
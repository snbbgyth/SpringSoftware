using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Core.QueueDAL;

namespace SpringSoftware.Web.DAL.Manage
{
    public class CommentManage
    {
        private static  ICommentDal _commentDal;

        static CommentManage()
        {
            _commentDal = DependencyResolver.Current.GetService<ICommentDal>();
        }

        public static IEnumerable<Comment> LastComments(int count)
        {
            try
            {
                return _commentDal.QueryLast(count);
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(typeof(CommentManage), MethodBase.GetCurrentMethod().Name, ex);
                return new List<Comment>();
            }
         
        }
    }
}
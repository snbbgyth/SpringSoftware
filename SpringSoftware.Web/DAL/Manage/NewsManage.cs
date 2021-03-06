﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;

namespace SpringSoftware.Web.DAL.Manage
{
    public static  class NewsManage
    {
        private static INewsDal _newsDal;
        private static INewsTypeDal _newsTypeDal;

        static NewsManage()
        {
            _newsDal = DependencyResolver.Current.GetService<INewsDal>();
            _newsTypeDal = DependencyResolver.Current.GetService<INewsTypeDal>();
            _newsTypeList = _newsTypeDal.QueryAll();
        }

        private static IEnumerable<NewsType> _newsTypeList;

        public static  IEnumerable<NewsType> QueryAllNewsTypes()
        {
            return _newsTypeList;
        }

        public static NewsType QueryNewsTypeById(int id)
        {
           return  _newsTypeDal.FirstOrDefault(t => t.Id == id);
        }

        public static string QueryNewsTypeNameById(int id)
        {
            var entity= _newsTypeDal.FirstOrDefault(t => t.Id == id);
            if (entity == null) return string.Empty;
            return entity.Name;
        }

        public static void RefreshNewsType()
        {
            Task.Factory.StartNew(NewTaskRefreshNewsType);
        }

        public static void NewTaskRefreshNewsType()
        {
            _newsTypeList = _newsTypeDal.QueryAll();
        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Web.Areas.Admin.Models;

namespace SpringSoftware.Web.DAL.Manage
{
    public class ProductManage
    {
        private static IProductPictureDal _productPictureDal;
        private static IPictureDal _pictureDal;
        private static IProductDal _productDal;
        private static IProductTypeDal _productTypeDal;

        static ProductManage()
        {
            _pictureDal = DependencyResolver.Current.GetService<IPictureDal>();
            _productDal = DependencyResolver.Current.GetService<IProductDal>();
            _productPictureDal = DependencyResolver.Current.GetService<IProductPictureDal>();
            _productTypeDal = DependencyResolver.Current.GetService<IProductTypeDal>();
        }

        public static ProductManage Current
        {
            get { return new ProductManage(); }
        }

        public IEnumerable<Picture> GetPicturesById(int productId)
        {
            var mapList = _productPictureDal.QueryByFun(t => t.ProductId == productId);
            return _pictureDal.QueryByIds(mapList.Select(x => x.ProductId as dynamic));
        }

        public static Picture GetFirstPictureById(int productId)
        {
            var mapList = _productPictureDal.QueryByFun(t => t.ProductId == productId);
            if (mapList.Any())
                return _pictureDal.QueryById(mapList.FirstOrDefault(t => t.DisplayOrder == mapList.Min(x => x.DisplayOrder)).PictureId);
            return null;
        }

        public static async Task<ProductViewModel> GetByProductId(int? id)
        {
            var productView = new ProductViewModel();
            if (id == null)
            {
                return productView;
            }
            productView.Product = await _productDal.QueryByIdAsync(id);
            if (productView.Product == null)
            {
                return productView;
            }
            productView.Product.ProductTypeList = await _productTypeDal.QueryAllAsync();
            productView.ProductPictureList = new List<ProductPicture>(await _productPictureDal.QueryByFunAsync(t => t.ProductId == id));
            if (productView.ProductPictureList.Any())
            {
                foreach (var productPicture in productView.ProductPictureList)
                {
                    var picture = await _pictureDal.QueryByIdAsync(productPicture.PictureId);
                    if (picture != null)
                        productView.PictureList.Add(picture);
                }
            }
            return productView;
        }
    }
}
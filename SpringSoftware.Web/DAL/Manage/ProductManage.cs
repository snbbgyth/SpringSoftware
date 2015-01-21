using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;

namespace SpringSoftware.Web.DAL.Manage
{
    public class ProductManage
    {
        private IProductPictureDal _productPictureDal;
        private IPictureDal _pictureDal;
        private IProductDal _productDal;

        public ProductManage()
        {
            _pictureDal = DependencyResolver.Current.GetService<IPictureDal>();
            _productDal = DependencyResolver.Current.GetService<IProductDal>();
            _productPictureDal = DependencyResolver.Current.GetService<IProductPictureDal>();
        }

        public static ProductManage Current
        {
            get { return new ProductManage();}
        }

        public IEnumerable<Picture> GetPicturesById(int productId)
        {
            var mapList = _productPictureDal.QueryByFun(t => t.ProductId== productId);
            return _pictureDal.QueryByIds(mapList.Select(x => x.ProductId as dynamic));
        }

        public Picture GetFirstPictureById(int productId)
        {
            var mapList = _productPictureDal.QueryByFun(t => t.ProductId == productId);
            return _pictureDal.QueryById(mapList.FirstOrDefault(t=>t.DisplayOrder==mapList.Min(x => x.DisplayOrder)));
        }
    }
}
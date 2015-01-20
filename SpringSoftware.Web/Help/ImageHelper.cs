using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;

namespace SpringSoftware.Web.Help
{
    public class ImageHelper
    {
        private static IPictureDal _pictureDal;

        static ImageHelper()
        {
            _pictureDal = DependencyResolver.Current.GetService<IPictureDal>();
        }

        public static byte[] GetBytes(HttpPostedFileBase file)
        {
            var stream = file.InputStream;
            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);
            return fileBinary;
        }

        public static  string GetContentType(HttpPostedFileBase file)
        {
            var contentType = file.ContentType;
            var fileExtension = Path.GetExtension(file.FileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            if (String.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = "image/bmp";
                        break;
                    case ".gif":
                        contentType = "image/gif";
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = "image/tiff";
                        break;
                    default:
                        break;
                }
            }
            return contentType;
        }

        public static void GenerateThumbnail(Picture picture)
        {
            try
            {
                SaveImage(picture, 280);
                SaveImage(picture, 125);
                SaveImage(picture, 100);
            }
            catch (Exception ex)
            {
            }
        }

        public static  string GetOriginalImagePath(Picture picture)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Images\\SaveUpload\\Product\\",picture.Id + Path.GetExtension(picture.FileName));
        }

        private static string GetThumbnailPath(Picture picture, int size)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Images\\SaveUpload\\Product\\Thumbnails",picture.Id + "_" + size + Path.GetExtension(picture.FileName));
        }

        public static string Get280PathByPicture(Picture picture)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images\\SaveUpload\\Product\\Thumbnails", picture.Id + "_" + 280 + Path.GetExtension(picture.FileName));
        }

        public static string Get280PathByPictureId(int pictureId,IEnumerable<Picture> pictureList=null)
        {
            Picture picture = null;
            if (pictureList != null && pictureList.Any())
            {
                picture = pictureList.FirstOrDefault(t => t.Id == pictureId);
            }
            if(picture==null)
              picture = _pictureDal.QueryById(pictureId);
            var path = Path.Combine(VirtualPathUtility.ToAbsolute("~/Images/SaveUpload/Product/Thumbnails/"),  picture.Id + "_" + 280 + Path.GetExtension(picture.FileName));
            return ResolveServerUrl(path, false);
        }

        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        } 

        private static void SaveImage(Picture picture, int size)
        {
            try
            {
                using (Image image = new Bitmap(GetOriginalImagePath(picture)))
                using(Image pThumbnail = image.GetThumbnailImage(size, size, ThumbnailCallback, IntPtr.Zero))
                pThumbnail.Save(GetThumbnailPath(picture, size));
            }
            catch (Exception ex)
            {

            }
        }

        public static bool ThumbnailCallback()
        {
            return true;
        }
    }
}
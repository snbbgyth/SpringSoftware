using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.DbModel;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Core.QueueDAL;

namespace SpringSoftware.Core.DAL
{
    public class ShopCartItemDal : DataOperationActivityBase<ShopCartItem>, IShopCartItemDal
    {
        public int SubmitById(int id)
        {
            int reslut = 0;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {

                    var queryString = string.Format(" Update {0} set IsSubmit=1 , LastModifyDate=:lastModifyDate where Id = :id ", typeof(ShopCartItem).Name);
                    reslut = session.CreateQuery(queryString)
                                    .SetParameter("id", id)
                                    .SetParameter("lastModifyDate", DateTime.Now)
                                    .ExecuteUpdate();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return reslut;
        }

    }
}

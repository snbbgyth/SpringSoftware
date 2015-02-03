using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.DbModel;

namespace SpringSoftware.Core.IDAL
{
    public interface IShopCartItemDal : IDataOperationActivity<ShopCartItem>
    {
        int SubmitById(int id);
    }
}

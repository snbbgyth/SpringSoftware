using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpringSoftware.Core.IDAL
{
    public interface IDataOperationActivity<T>
    {
        void Insert(T entity);

        void SaveOrUpdate(T entity);

        void Modify(T entity);

        IList<T> QueryByFun(Expression<Func<T, bool>> fun);

        int DeleteById(string id);

        List<T> QueryAll();

        T QueryById(string id);

        int DeleteAll();

        T FirstOrDefault();

        T FirstOrDefault(Expression<Func<T, bool>> fun);

    }
}

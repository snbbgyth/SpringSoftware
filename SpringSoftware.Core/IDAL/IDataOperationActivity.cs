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
        int Insert(T entity);

        int SaveOrUpdate(T entity);

        int Modify(T entity);

        IList<T> QueryByFun(Expression<Func<T, bool>> fun);

        int DeleteById(string id);

        List<T> QueryAll();

        T QueryById(string id);

        int DeleteAll();

        T FirstOrDefault();

        T FirstOrDefault(Expression<Func<T, bool>> fun);

         Task<int> InsertAsync(T entity);

         Task<int> SaveOrUpdateAsync(T entity);

         Task<int> ModifyAsync(T entity);

        Task<IList<T>> QueryByFunAsync(Expression<Func<T, bool>> fun);

        Task<int> DeleteByIdAsync(string id);

        Task<List<T>> QueryAllAsync();

        Task<T> QueryByIdAsync(string id);

        Task<int> DeleteAllAsync();

        Task<T> FirstOrDefaultAsync();

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> fun);

    }
}

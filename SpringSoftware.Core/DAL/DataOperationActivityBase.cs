using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpringSoftware.Core.IDAL;
using SpringSoftware.Core.QueueDAL;

namespace SpringSoftware.Core.DAL
{
    public abstract class DataOperationActivityBase<T> : IDataOperationActivity<T> where T : class, new()
    {
        #region Private Variable

        #endregion

        #region Private Property

        #endregion

        public DataOperationActivityBase()
        {

        }

        public virtual int Insert(T entity)
        {
            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    session.Save(entity);
                    session.Flush();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
        }

        public virtual int SaveOrUpdate(T entity)
        {
            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    session.SaveOrUpdate(entity);
                    session.Flush();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
        }

        public virtual int  Modify(T entity)
        {
            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    session.Update(entity);
                    session.Flush();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
                return 0;
            }
        }

        public virtual IList<T> QueryByFun(Expression<Func<T, bool>> fun)
        {
            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    if (fun != null)
                    {
                        return session.QueryOver<T>().Where(fun).List();
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return null;
        }

        public virtual int Delete(dynamic entity)
        {
            int result = 0;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    var queryString = string.Format("delete {0} where Id = :id",
                                                    typeof(T).Name);
                    result = session.CreateQuery(queryString).SetParameter("id", entity.Id).ExecuteUpdate();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public virtual int DeleteById(string id)
        {
            int reslut = 0;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    var queryString = string.Format("delete {0} where Id = :id",
                                                    typeof(T).Name);
                    reslut = session.CreateQuery(queryString)
                                    .SetParameter("id", id)
                                    .ExecuteUpdate();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return reslut;
        }

        public virtual List<T> QueryAll()
        {
            List<T> entityList = null;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    entityList = session.CreateCriteria(typeof(T)).List<T>().ToList();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return entityList;
        }

        public virtual T QueryById(string id)
        {
            T entity = default(T);

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    entity = session.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return entity;
        }

        public virtual int DeleteAll()
        {
            int result = 0;

            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    var queryString = string.Format("from {0} ", typeof(T).Name);
                    result = session.Delete(queryString);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        public T FirstOrDefault()
        {
            try
            {
                using (var session = FluentNHibernateDal.Instance.GetSession())
                {
                    return session.QueryOver<T>().SingleOrDefault();
                }

            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return default(T);
        }

        /// <summary>
        /// Find first or default by funcation
        /// </summary>
        /// <param name="fun">condition funcation</param>
        /// <returns>ClientTableState entity</returns>
        public T FirstOrDefault(Expression<Func<T, bool>> fun)
        {

            try
            {
                var entityList = QueryByFun(fun);
                if (entityList != null && entityList.Any()) return entityList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogInfoQueue.Instance.Insert(GetType(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return null;
        }


        public async Task<int> InsertAsync(T entity)
        {
            var task = Task.Factory.StartNew(() => Insert(entity));
           return  await task;
           
        }

        public async Task<int> SaveOrUpdateAsync(T entity)
        {
            var task = Task.Factory.StartNew(() => SaveOrUpdate(entity));
           return  await task;
        }

        public async Task<int> ModifyAsync(T entity)
        {
            var task = Task.Factory.StartNew(() => Modify(entity));
            return  await task;
        }

        public async Task<IList<T>> QueryByFunAsync(Expression<Func<T, bool>> fun)
        {
            var task = Task.Factory.StartNew(() => QueryByFun(fun));
            return  await task;
        }

        public async Task<int> DeleteByIdAsync(string id)
        {
            var task = Task.Factory.StartNew(() => DeleteById(id));
            return await task;
        }

        public async Task<List<T>> QueryAllAsync()
        {
            var task = Task.Factory.StartNew(() => QueryAll());
            return await task;
        }

        public async Task<T> QueryByIdAsync(string id)
        {
            var task = Task.Factory.StartNew(() => QueryById(id));
            return await task;
        }

        public async Task<int> DeleteAllAsync()
        {
            var task = Task.Factory.StartNew(() => DeleteAll());
            return await task;
        }

        public async Task<T> FirstOrDefaultAsync()
        {
            var task = Task.Factory.StartNew(() => FirstOrDefault());
            return await task;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> fun)
        {
            var task = Task.Factory.StartNew(() => FirstOrDefault(fun));
            return await task;
        }
    }
}

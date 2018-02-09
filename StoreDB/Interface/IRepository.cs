using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StoreDB.Interface
{
    public interface IRepository<T> where T : class
    {
        #region Unit of Work
        /// <summary>
        /// unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 取得單一 entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> filter);
         

        /// <summary>
        /// GetAll.
        /// </summary>
        IEnumerable<T> GetAll();
        /// <summary>
        /// 搜尋
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> filter); 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// save change
        /// </summary>
        void Commit();

        /// <summary>
        /// 回傳頁數
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        //IPagedList<T> ReturnPageList(IEnumerable<T> toList, int currentPage, int PageSize);
        #endregion
        #region none unit of work    
        ///// <summary>
        ///// 取得單一 entity
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //T GetSingle(Expression<Func<T, bool>> filter);

        ///// <summary>
        ///// GetAll.
        ///// </summary>
        //IEnumerable<T> GetAll();

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="entity"></param>
        //void Create(T entity);

        ///// <summary>
        ///// 刪除
        ///// </summary>
        ///// <param name="entity"></param>
        //void Delete(T entity);

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="entity"></param>
        //void Update(T entity);
        #endregion

    }
}

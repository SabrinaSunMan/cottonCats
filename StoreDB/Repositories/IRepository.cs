using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDB.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 取得單一 entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetSingle(int id);

        /// <summary>
        /// GetAll.
        /// </summary>
        IEnumerable<T> GetAll();

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
        /// 回傳頁數
        /// </summary>
        /// <param name="toList"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        //IPagedList<T> ReturnPageList(IEnumerable<T> toList, int currentPage, int PageSize);

    }
}

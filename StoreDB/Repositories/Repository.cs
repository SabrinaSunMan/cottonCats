using System.Collections.Generic;
using System.Linq;
using StoreDB.Model;
using System.Data.Entity;
using StoreDB.Interface;
using System;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Reflection;
using System.ComponentModel;

namespace StoreDB.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbSet<T> _Objectset;

        private DbSet<T> ObjectSet
        {
            get
            {
                if (_Objectset == null)
                {
                    _Objectset = UnitOfWork.Context.Set<T>();
                }
                return _Objectset;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Create(T entity)
        {
            ObjectSet.Add(entity);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            ObjectSet.Remove(entity);
        }

        /// <summary>
        /// GetAll.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return ObjectSet;
        }

        /// <summary>
        /// 搜尋
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.Where(filter);
        }

        /// <summary>
        /// 取得單一 entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        /// <summary>
        /// 欄位 與 值 是否匹配.
        /// </summary>
        /// <param name="FiledName">Name of the filed.</param>
        /// <param name="MatchValue">The match value.</param>
        /// <returns></returns>
        public bool GetSingleMatch(string FiledName, string MatchValue)
        {
            // 取得 Table [Member] 所有屬性 (包含欄位名稱、欄位型態等資料)
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name == FiledName) // 找到符合的欄位
                {
                    // Member.Where(x=>x.Email == "jane199141") 這是運算式 Lambda
                    // 可轉換稱作 [運算式樹狀結構] expresstion tree
                    // 什麼是 expresstion tree ? 白話文: 將程式碼當作樹狀結構資料, 並可動態修改Runtime的程式碼, 動態查詢.
                    // 舉例：var sum = 1+2
                    // var sum 隱含變數類型宣告.
                    // = 指派運算子
                    // 1+2 (二元加法)運算式, 1、2 運算元, + 運算子

                    // ParameterExpression 具名參數運算式
                    // 增加一個型態是 Member (因為我要搜尋這個Table) 的節點名稱為 x
                    // https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.expressions.expression.parameter?view=netframework-4.7.2
                    ParameterExpression param = Expression.Parameter(typeof(T), "x");

                    #region Convert to specific data type

                    // Expresstion 底下的成員.代表存取欄位或屬性
                    // 要將用反射取得的 Member.Email 綁上 Line:251 所敘述之具名參數運算式 (準備用 Express底下 Email成員去作運算)
                    // https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.expressions.memberexpression?view=netframework-4.7.2
                    MemberExpression member = Expression.Property(param, prop.Name);
                    // 取得有效的 運算式
                    UnaryExpression valueExpression = GetValueExpression(prop.Name, MatchValue, member);

                    #endregion Convert to specific data type

                    // 建立相等的 Expression
                    Expression body = Expression.Equal(member, valueExpression);

                    var final = Expression.Lambda<Func<T, bool>>(body: body, parameters: param);

                    var func = final.Compile();
                    //compiles the expression tree to a func delegate

                    var GetSingleData = GetSingle(final);

                    return GetSingleData == null ? false : true;
                }
            }
            return false;
        }

        private UnaryExpression GetValueExpression(string propertyName, string val, MemberExpression member)
        {
            // PropertyInfo 是反射會用到的其中一個方法, 是一個容器的概念，將指定的欄位 Property. Attribute. Metadata.放置其中
            var propertyType = ((PropertyInfo)member.Member).PropertyType;// 取得這個屬性的類別

            // TypeDescriptor.GetConverter 是一個Method 傳回元件或類型的類型轉換子。
            // 這邊不寫死要打算把這個欄位轉成什麼型態，應是由 DB Filed 自行決定，以這個範例來看就是 Member.Email的欄位型態
            var converter = TypeDescriptor.GetConverter(propertyType);

            // 如果不能轉型成 string 吐出錯誤
            if (!converter.CanConvertFrom(typeof(string))) throw new NotSupportedException();

            // 這個欄位打算塞什麼值，以之前 Line : 286 定義好的型態去轉型
            var propertyValue = converter.ConvertFromInvariantString(val);

            // 建立 ConstantExpression (代表具有常數值的運算式)
            // ex : y = 3 * 29; 而 Constant 就是 3*29
            // https://docs.microsoft.com/zh-tw/dotnet/api/system.linq.expressions.expression.constant?view=netframework-4.7.2
            var constant = Expression.Constant(propertyValue);

            // UnaryExpression : Class 隸屬於 System.Linq.Expressions 底下
            // 	轉型或轉換作業 成 具有一元運算子的運算式
            return Expression.Convert(constant, propertyType);
        }

        /// <summary>
        /// save change
        /// </summary>
        public void Commit()
        {
            UnitOfWork.Save();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="keyValues">The key values.</param>
        protected void Update(T entity, params object[] keyValues)
        {
            var entry = UnitOfWork.Context.Entry<T>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = UnitOfWork.Context.Set<T>();
                T attachedEntity = set.Find(keyValues);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = UnitOfWork.Context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
            //var entry = Context.Entry(entity);
            //entry.State = System.Data.EntityState.Modified;
            //ObjectSet.Attach(entity);
        }

        //public virtual void Update(T entity)
        //{
        //    //DbEntityEntry dbEntityEntry = Context.Entry(entity);
        //    //if (dbEntityEntry.State == EntityState.Detached)
        //    //{
        //    //    DbSet.Attach(entity);
        //    //}
        //    //DbSet.Attach(entity);
        //    ObjectSet.Attach(entity);
        //}

        #region None Unit of work

        //private readonly StoreDBContext _DBContex;

        //private DbSet<T> Dbset = null;

        //public Repository()
        //{
        //    _DBContex = new StoreDBContext();
        //    Dbset = _DBContex.Set<T>();
        //}

        //public void Create(T entity)
        //{
        //    Dbset.Add(entity);
        //    _DBContex.SaveChanges();
        //}

        //public void Delete(T entity)
        //{
        //    Dbset.Remove(entity);
        //    _DBContex.SaveChanges();
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    return Dbset.ToList();
        //}

        ////public T GetSingle(string id)
        ////{
        ////    return Dbset.Find(id);
        ////}
        //public T GetSingle(Expression<Func<T, bool>> filter)
        //{
        //    return Dbset.SingleOrDefault(filter);
        //}

        //public void Update(T entity)
        //{
        //    _DBContex.Entry(entity).State = EntityState.Modified;
        //    _DBContex.SaveChanges();
        //}

        #endregion None Unit of work
    }
}
using System.Collections.Generic;
using System.Linq;
using StoreDB.Model;
using System.Data.Entity;
using StoreDB.Interface;
using System;
using System.Linq.Expressions;

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
        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public void Create(T entity)
        {
            ObjectSet.Add(entity);
        }

        public void Delete(T entity)
        {
            ObjectSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return ObjectSet;
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        public void Commit()
        {
            UnitOfWork.Save();
        }

        public virtual void Update(T entity)
        {
            ObjectSet.Attach(entity); 
        } 

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

        //public T GetSingle(string id)
        //{
        //    return Dbset.Find(id);
        //}


        //public void Update(T entity)
        //{
        //    _DBContex.Entry(entity).State = EntityState.Modified;
        //    _DBContex.SaveChanges();
        //}
        #endregion
    }
}

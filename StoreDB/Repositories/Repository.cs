using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDB.Model;
using System.Data.Entity;

namespace StoreDB.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StoreDBContext _DBContex;
        
        private DbSet<T> Dbset = null;

        public Repository()
        {
            _DBContex = new StoreDBContext(); 
            Dbset = _DBContex.Set<T>();
        }

        public void Create(T entity)
        {
            Dbset.Add(entity);
            _DBContex.SaveChanges();
        }

        public void Delete(T entity)
        {
            Dbset.Remove(entity);
            _DBContex.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Dbset.ToList();
        }

        public T GetSingle(int id)
        {
            return Dbset.Find(id);
        }

        public void Update(T entity)
        {
            _DBContex.Entry(entity).State = EntityState.Modified;
            _DBContex.SaveChanges();
        }

    }
}

using StoreDB.Interface;
using StoreDB.Model.Partials;
using StoreDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StoreDB.Interface
{
    public interface IZipCode : IDisposable
    {
        bool IsExists(Expression<Func<ZipCode, bool>> predicate);

        int TotalCount(Expression<Func<ZipCode, bool>> predicate);

        ZipCode FindOne(int id);

        ZipCode FindOneByPostalCode(int postalCode);

        IQueryable<ZipCode> FindAll();

        IQueryable<ZipCode> Find(Expression<Func<ZipCode, bool>> predicate);

        Dictionary<string, string> GetAllCities();

        Dictionary<string, string> GetAllCityDictinoary();

        Dictionary<string, string> GetCountyByCityName(string cityName);
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Realms;

namespace StarsForward.Data.Interfaces
{
    public interface IRepository<T> where T : RealmObject
    {
        T GetById(string id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void Delete(T entity);
        void DeleteById(string id);
        //void Edit(T entity);
        T Find(Expression<Func<T, bool>> predicate);
    }
}
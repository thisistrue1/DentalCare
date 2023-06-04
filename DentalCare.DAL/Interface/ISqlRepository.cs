using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace DentalCare.DAL.Interface
{
    public interface ISqlRepository<T>
                    where T : class
    {
        ObjectSet<T> Set { get; }
        IQueryable<T> FindAll();
        IQueryable<T> FindAll(string ChildObjectToInclude);
        IEnumerable<T> FindAllAsEnumerable();

        IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);
        T GetSingleById(Expression<Func<T, bool>> predicateId);
        int Add(T newEntity);
        //void Add(T newEntity, bool Commit = true);
        int Add(T newEntity, bool commit);
        void RemoveRange(ICollection<T> entities, bool commit);
        void Remove(T entity);
        void Remove(T entity, bool commit);
        //void Update(T entity);
        //void Update(T entity, bool Commit);
        int Update(T entity);
        int Update(T entity, bool Commit);
        void Update(T entity, bool Commit, bool Attach);
        int ExecuteStoreCommand(string commendText, params object[] parameters);
        IEnumerable<S> ExecuteStoreQuery<S>(string commendText, string tablename, MergeOption merge, params SqlParameter[] parameters);
        IEnumerable<S> ExecuteStoreQuery<S>(string commendText, params SqlParameter[] parameters);
        void CommitTransaction();
    }

}

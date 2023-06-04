using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DentalCare.DAL.Interface;

namespace DentalCare.DAL
{
    public class SqlRepository<T> : ISqlRepository<T>
                                    where T : class
    {

        public SqlRepository(ObjectContext context)
        {
            _objectSet = context.CreateObjectSet<T>();
            _context = context; //
            _context.CommandTimeout = 60;
        }

        public ObjectSet<T> Set
        {
            get
            {
                return _objectSet;

            }
        }

        public IQueryable<T> FindAll()
        {
            return _objectSet;
        }
        public IQueryable<T> FindAll(string ChildObjectToInclude)
        {
            return _objectSet.Include(ChildObjectToInclude);
        }
        public IEnumerable<T> FindAllAsEnumerable()
        {
            return _objectSet;
        }
        public IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate)
        {
            //if (_context.Connection.State == ConnectionState.Broken)
            //{
            //    _context.Connection.Close();
            //}
            //if (_context.Connection.State != ConnectionState.Open)
            //{
            //    _context.Connection.Open();
            //}
            return _objectSet.Where(predicate);
        }

        public T GetSingleById(Expression<Func<T, bool>> predicateId)
        {
            return _objectSet.Where(predicateId).FirstOrDefault();
        }
        public int Add(T newEntity)
        {
            return (Add(newEntity, true));
        }
        public int Add(T newEntity, bool commit)
        {
            _objectSet.AddObject(newEntity);
            if (commit)
            {
                try
                {
                    return (_objectSet.Context.SaveChanges());
                }
                catch (Exception ex)
                {
                    _objectSet.Detach(newEntity);
                    throw ex;
                }

            }
            return (0);
        }

        public void RemoveRange(ICollection<T> entities, bool commit)
        {
            foreach (var entity in entities)
            {
                _objectSet.DeleteObject(entity);
            }
            if (commit)
            {
                _objectSet.Context.SaveChanges();
            }
        }

        //public void Add(T newEntity, bool Commit)
        //        {
        //            _objectSet.AddObject(newEntity);
        //            _objectSet.Context.SaveChanges();

        //            if (Commit)
        //            {
        //                _objectSet.Context.SaveChanges();
        //            }            
        //        }
        public void Remove(T entity)
        {
            Remove(entity, true);

        }

        public void Remove(T entity, bool commit)
        {
            _objectSet.DeleteObject(entity);
            if (commit)
            {
                _objectSet.Context.SaveChanges();
            }
        }

        public int Update(T entity)
        {
            return Update(entity, true);
        }

        public int Update(T entity, bool commit)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            try
            {
                _objectSet.Attach(entity);
            }
            catch (Exception) { }
            _objectSet.Context.ObjectStateManager.ChangeObjectState(entity, System.Data.Entity.EntityState.Modified);
            if (commit) return (_objectSet.Context.SaveChanges());
            return (0);
        }

        //public void Update(T entity)
        //       {
        //           if (entity == null)
        //               throw new ArgumentNullException("entity");

        //           _objectSet.Attach(entity);
        //           _objectSet.Context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
        //           _objectSet.Context.SaveChanges();
        //       }

        //public void Update(T entity, bool Commit)
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException("entity");

        //    _objectSet.Attach(entity);
        //    _objectSet.Context.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
        //    _objectSet.Context.SaveChanges();
        //}

        public void Update(T entity, bool Commit, bool Attached)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (!Attached)
            {
                _objectSet.Attach(entity);
                _objectSet.Context.ObjectStateManager.ChangeObjectState(entity, System.Data.Entity.EntityState.Modified);
                if (Commit) { _objectSet.Context.SaveChanges(); };
            }
            else
            {
                _objectSet.ApplyCurrentValues(entity);
                if (Commit) { _objectSet.Context.SaveChanges(); };
            }
        }


        public int ExecuteStoreCommand(string commendText, params object[] parameters)
        {
            return _context.ExecuteStoreCommand(commendText, parameters);
        }

        public IEnumerable<S> ExecuteStoreQuery<S>(string commendText, string tablename, MergeOption merge, params SqlParameter[] parameters)
        {
            return _objectSet.Context.ExecuteStoreQuery<S>(commendText, tablename, merge, parameters);
        }

        public IEnumerable<S> ExecuteStoreQuery<S>(string commendText, params SqlParameter[] parameters)
        {
            return _objectSet.Context.ExecuteStoreQuery<S>(commendText, parameters);
        }


        public void CommitTransaction()
        {
            _objectSet.Context.AcceptAllChanges();
        }

        protected ObjectSet<T> _objectSet;
        protected ObjectContext _context;
    }
}
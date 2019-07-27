using AvenueOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Utilities
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //context TODO; 

        //temporary
        private List<TEntity> context;

        public Repository(List<TEntity> entities)
        {
            context = entities;
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Object argument to be added cannot be null.");

            //connect to database

            //check if user exist
            TEntity e = context.Find(ent => ent.Equals(entity));
            bool doesExist = e != null;

            if (doesExist)
                throw new ArgumentException("Cannot add a user that already exists.");

            //add user
            context.Add(entity);
            bool hasAddedUser = true;

            //successfully added user
            if (!hasAddedUser)
                throw new Exception("Could not add user.");
        }

        public void AddRange(IEnumerable<TEntity> collection)
        {
            foreach (TEntity item in collection)
            {
                context.Add(item);
            }
        }

        //public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        public TEntity Find(Predicate<TEntity> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate cannot be null");

            return context.Find(predicate);

        }

        //public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        public TEntity Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context;
        }

        public void Remove(TEntity entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}

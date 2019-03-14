using MyAddressBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAddressBookAPI.Tests.Fakes
{
    /// <summary>
    /// This code was sourced from the internet quite a 
    /// while ago.  I made some minor customisations.
    /// </summary>
    public class FakeDbContext : IDbContext
    {
        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
        public List<object> Added = new List<object>();
        public List<object> Updated = new List<object>();
        public List<object> Removed = new List<object>();
        public bool Saved = false;

        public string ConnectionString { get; } = null;

        // A Dictionary of custom primary keys indexed by the Entity type -
        // used for the Find method
        private Dictionary<Type, string> customPrimaryKey =
            new Dictionary<Type, string>();

        public IQueryable<T> Query<T>() where T : class
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }

        public void AddSet<T>(IQueryable<T> objects)
        {
            Sets.Add(typeof(T), objects);
        }

        public void Add<T>(T entity) where T : class
        {
            Added.Add(entity);

            // Create a new List that contains the added entity
            List<T> lstWithNewEntity = new List<T>();
            lstWithNewEntity.Add(entity);

            if (!Sets.ContainsKey(typeof(T)))
            {
                Sets.Add(typeof(T), lstWithNewEntity.AsQueryable());
            }
            else
            {
                Sets[typeof(T)] = ((IQueryable<T>)Sets[typeof(T)]).AsEnumerable().Concat(lstWithNewEntity).AsQueryable();
            }
        }

        public void Update<T>(T entity) where T : class
        {
            Updated.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            Removed.Add(entity);
            Sets[typeof(T)] = Query<T>().Where(t => t != entity);

            if (((IQueryable<T>)Sets[typeof(T)]).Count() == 0)
                Sets.Remove(typeof(T));
        }

        public void AddFindConfig_CustomPrimaryKey<T>(string strPrimaryKey)
        {
            customPrimaryKey.Add(typeof(T), strPrimaryKey);
        }

        /// <summary>
        /// We need to implement an in memory "Find".  This will only
        /// work if the primary key field for the entity is in the form of 
        /// EntityName + "Id" or if a custom primary key has been added for the 
        /// entity at setup using the AddFindConfig_CustomPrimaryKey method.
        /// </summary>
        /// <typeparam name="T">
        /// The Entity against which the find is to be performed.
        /// </typeparam>
        /// <param name="keyValues">
        /// The key value.
        /// </param>
        /// <returns>
        /// The entity with the given primary key.
        /// </returns>
        public T Find<T>(params object[] keyValues) where T : class
        {
            // Get the primary key - check for a custom naming convention first...
            string primaryKey;

            if (customPrimaryKey.ContainsKey(typeof(T)))
                primaryKey = customPrimaryKey[typeof(T)];
            else
                primaryKey = typeof(T).Name + "Id";

            // Build lamda
            var x = Expression.Parameter(typeof(T), "x");
            var primarykey = Expression.Property(x, primaryKey);
            var id = Expression.Constant((int)keyValues[0]);
            var equal = Expression.Equal(primarykey, id);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, x);
            var result = Query<T>().Where<T>(lambda).FirstOrDefault();
            return result;
        }

        public Task<T> FindAsync<T>(params object[] keyValues) where T : class
        {
            return Task.Run(() => Find<T>(keyValues));
        }

        public void SaveChanges()
        {
            Saved = true;
        }

        public Task SaveChangesAsync()
        {
            return Task.Run(() => Saved = true);
        }

        public void Dispose() { }
    }
}

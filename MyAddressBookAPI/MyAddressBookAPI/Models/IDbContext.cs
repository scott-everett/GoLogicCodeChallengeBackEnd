using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http;

namespace MyAddressBookAPI.Models
{
    /// <summary>
    /// Data context interface that is a common
    /// interface used by fake and real DbContexts.
    /// Allows for dependancy injection for testing.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        T Find<T>(params Object[] keyValues) where T : class;
        Task<T> FindAsync<T>(params Object[] keyValues) where T : class;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}

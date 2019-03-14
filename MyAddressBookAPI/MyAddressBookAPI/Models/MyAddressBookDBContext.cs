using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyAddressBookAPI.Models
{
    /// <summary>
    /// Main DbContext.
    /// </summary>
    public class MyAddressBookDBContext : DbContext, IDbContext
    {
        public MyAddressBookDBContext() : base("name=MyAddressBook")
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PhoneDetail> PhoneDetails { get; set; }

        IQueryable<T> IDbContext.Query<T>()
        {
            return Set<T>();
        }

        void IDbContext.Add<T>(T entity)
        {
            Set<T>().Add(entity);
        }

        void IDbContext.Update<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        void IDbContext.Remove<T>(T entity)
        {
            Set<T>().Remove(entity);
        }

        T IDbContext.Find<T>(params object[] keyValues)
        {
            return Set<T>().Find(keyValues);
        }

        async Task<T> IDbContext.FindAsync<T>(params object[] keyValues) 
        {
            return await Set<T>().FindAsync(keyValues);
        }

        void IDbContext.SaveChanges()
        {
            SaveChanges();
        }

        async Task IDbContext.SaveChangesAsync()
        {
            await SaveChangesAsync();
        }
    }
}
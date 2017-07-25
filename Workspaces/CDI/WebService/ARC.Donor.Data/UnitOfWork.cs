using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _context;
        private Logger log = LogManager.GetCurrentClassLogger();

        private bool _disposed;
        private Hashtable _repositories;
        
        public UnitOfWork(DBContext context)
        {
            this._context = context;            
        }

        public DBContext getContext()
        {
            return _context;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {

            if (!_disposed)
                if (disposing)
                    this._context.Dispose();
            this._disposed = true;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            try
            {
                if (this._repositories == null)
                    this._repositories = new Hashtable();
                var type = typeof(T).Name;
                if (!this._repositories.ContainsKey(type))
                {
                    var repositoryType = typeof(Repository<>);
                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this._context);
                    this._repositories.Add(type, repositoryInstance);
                }
                return (IRepository<T>)this._repositories[type];
            }
            catch (Exception ex)
            {
                string msg = "Unit of work repository Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
            return null;
        }

        public void Save()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = "SaveChanges Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
        }

    }
}
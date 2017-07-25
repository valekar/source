using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Transactions;
namespace DonorWebservice.ClientValidation
{
    public class Repository<T> :  IRepository<T> where T : class
    {
        private readonly DBContext _context;
        private IDbSet<T> _entities;
        private Logger log = LogManager.GetCurrentClassLogger();
        private string _message = string.Empty;

        public Repository()
        {
            this._context = new DBContext();
            //this._context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public Repository(DBContext context)
        {
            this._context = context;
            //this._context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public string GetMessage()
        {
            return _message;
        }
        public IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includePropeties = null,
            int? page = null,
            int? size = null
            )
        {
            try
            {
                IQueryable<T> query = this.Entities;
                if (includePropeties != null)
                    includePropeties.ForEach(i => { query.Include(i); });
                if (filter != null)
                    query = query.Where(filter);
                if (orderBy != null)
                    query = orderBy(query);
                if (page != null && size != null)
                {
                    query = query.Skip((page.Value - 1) * size.Value)
                        .Take(size.Value);
                }

                return query;
            }
            catch (Exception ex)
            {
                string msg = "Get Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
            return null;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] include)
        {
            try
            {
                IQueryable<T> query = this.Entities;

                if (include != null && include.Count() > 0)
                    foreach (string inc in include)
                    {
                        query = query.Include(inc);
                    }

                return query.Where(predicate);
            }
            catch (Exception ex)
            {
                string msg = "FindBy Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
            return null;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public IList<T> GetAll()
        {
            return this.Entities.ToList();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                //_context.Configuration.LazyLoadingEnabled = true;
                //this._context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                return Table.Where(predicate);
            }
            catch (Exception ex)
            {
                string msg = "Find Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
            return null;
        }

        public async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => Find(predicate));
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return Table.Where(predicate).SingleOrDefault();
            }
            catch (Exception ex)
            {
                string msg = "Single Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
            return null;
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => First(predicate));
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            try
            {
                //this._context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var result = Table.Where(predicate).AsQueryable(); //.FirstOrDefault();
                return result.FirstOrDefault();
                //return Table.Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string msg = "First Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
            return null;
        }

        public int Insert(T entity)
        {
            return this.Insert(entity, true);
        }

        public TT Insert<TT>(T entity) where TT : IConvertible
        {
            var result = this.Insert(entity, true);
            return (TT)Convert.ChangeType(result, typeof(TT));
        }

        public async Task<int> InsertAsync(T entity)
        {
            return await InsertAsync(entity, true);
        }

        public int Insert(T entity, bool Commit)
        {
            _message = "";
            try
            {
                if (entity == null)
                {
                    log.Info("Error: Entity is NULL " + entity.GetType().Name.ToString());
                    //throw new ArgumentNullException("entity");
                    _message = "Entity to insert is null";
                    return -1;
                }
                this.Entities.Add(entity);
                if (Commit) return this._context.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;                        
                    }
                }
                _message = msg;
                log.Info("Insert Error: " + msg);
                //var fail = new Exception(msg, dbEx);
                //throw fail;
            }
            catch (Exception ex)
            {
                string msg = "Insert Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception:" + ex.InnerException.ToString();
                _message = msg;
                log.Info("Error: " + msg);
            }
            return -1;
        }

        public async Task<int> InsertAsync(T entity, bool Commit)
        {
            try
            {
                if (entity == null)
                {
                    log.Info("Error: Entity is NULL " + entity.GetType().Name.ToString());
                    //throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                if (Commit) return await Task.Run(() => this._context.SaveChanges());
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                log.Info("Insert Error: " + msg);
                //var fail = new Exception(msg, dbEx);
                //throw fail;
            }
            catch (Exception ex)
            {
                string msg = "Insert Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception:" + ex.InnerException.ToString();
                log.Info("Error: " + msg);
            }
            return 0;
        }

        public void Update(T entity)
        {
            this.Update(entity, true);
        }

        public void Update(T entity, bool Commit)
        {
            try
            {
                if (entity == null)
                {
                    log.Info("Error: Entity is NULL " + entity.GetType().Name.ToString());
                    //throw new ArgumentNullException("entity");
                }

                if (Commit) this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                log.Info("Update Error: " + msg);
                //var fail = new Exception(msg, dbEx);
                //throw fail;
            }
            catch (Exception ex)
            {
                string msg = "Update Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception:" + ex.InnerException.ToString();
                log.Info("Error: " + msg);
            }
        }

        public void Delete(T entity)
        {
            this.Delete(entity, true);
        }

        public async void SaveChangesAsync()
        {
            try
            {
                //await this._context.SaveChangesAsync();
                await Task.Run(() => this._context.SaveChanges());
            }
            catch (Exception ex)
            {
                string msg = "SaveChanges Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception: " + ex.InnerException.ToString();
                log.Info(msg);
            }
        }

        public void SaveChanges()
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

        public void Delete(T entity, bool Commit)
        {
            try
            {
                if (entity == null)
                {
                    log.Info("Error: Entity is NULL " + entity.GetType().Name.ToString());
                    //throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(entity);
                if (Commit) this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                log.Info("Delete Error: " + msg);
                //var fail = new Exception(msg, dbEx);
                //throw fail;
            }
            catch (Exception ex)
            {
                string msg = "Delete Error: " + ex.Message;
                if (ex.InnerException != null) msg += " Inner Exception:" + ex.InnerException.ToString();
                log.Info("Error: " + msg);
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public virtual IEnumerable<T> TableE
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }

        private List<T> Changed = new List<T>();
        private List<T> Del = new List<T>();
        private List<T> New = new List<T>();

        public void AddScope(T obj)
        {
            New.Add(obj);
        }

        public void UpdateScope(T obj)
        {
            Changed.Add(obj);
        }

        public void DeleteScope(T obj)
        {
            Del.Add(obj);
        }

        public void Committ()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (T o in Changed)
                {
                    this.Update(o, false);
                    // o.Update();
                }
                foreach (T o in New)
                {
                    this.Insert(o, false);
                    //o.Insert();
                }
                foreach (T o in Del)
                {
                    this.Delete(o, false);
                    //o.Insert();
                }
                scope.Complete();
            }
        }

    }
}
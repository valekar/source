using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DonorWebservice.ClientValidation
{
    public interface IRepository<T> where T : class
    {
        //T GetById(object id);
        //int Insert(T entity);
        //void Update(T entity);
        //void Delete(T entity);
        //int  Insert(T entity, bool Commit);
        //void Update(T entity, bool Commit);
        //void Delete(T entity, bool Commit);
        //IQueryable<T> Table { get; }
        //IEnumerable<T> TableE { get; }
        //IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        //T Single(Expression<Func<T, bool>> predicate);
        //T First(Expression<Func<T, bool>> predicate);
        //IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] include);
        //void SaveChanges();
        //Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        //Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        //Task<int> InsertAsync(T entity);
        //Task<int> InsertAsync(T entity, bool Commit);
        //void SaveChangesAsync();
        IQueryable<T> Get(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          List<Expression<Func<T, object>>> includePropeties = null,
          int? page = null,
          int? size = null
          );
        int Insert(T entity);
        int Insert(T entity, bool Commit);
        Task<int> InsertAsync(T entity, bool Commit);
        void SaveChangesAsync();
        void SaveChanges();
        string GetMessage();
    }
}

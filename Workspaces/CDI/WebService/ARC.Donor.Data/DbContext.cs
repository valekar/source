using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ARC.Donor.Data
{
    public static class ExecToListExt
    {
        public static List<T> ExecToList<T>(this IQueryable<T> query, DBContext DbContext)
        {
            //return query.ToList();
            string qry = query.ToString();
            qry = qry.Replace("[dbo]", "dw_stuart_vws").Replace("[", "").Replace("]", "");
            if (query == null) return null;
            var result = DbContext.Database.SqlQuery<T>(qry).ToList();
            return result;
        }
    }

    public class DBContext:DBContextBase, IStuartDbContext
    {
        public DBContext()
        {
            Database.SetInitializer<DBContext>(null);
        } 
        public DBContext(string conn)
            : base(conn)          
        {
            Database.SetInitializer<DBContext>(null);
        }        

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

    }
}
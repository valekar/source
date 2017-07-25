using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DonorWebservice.ClientValidation
{
     public partial class DBContext: DbContext
    {
        public DBContext()
             : base("name=SQLSrvrConnection")
        {
            
        }

        public DBContext(string conn)
            : base(conn)
        {
            
        }

        public virtual DbSet<ClientInfo> ClientInfo { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<QueryTimeLogger> QueryTimeLogger { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service
{
    public class QueryLogEventArgs : EventArgs
    {
        public string Query { get; set; }
        public string Action { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string UserName { get; set; }
    }

    public abstract class QueryLogger
    {
        public event EventHandler<QueryLogEventArgs> InsertQueryLogEvent;

        protected virtual void OnInsertQueryLogger(string Action, string Qry, DateTime STime, DateTime? ETime)
        {
            EventHandler<QueryLogEventArgs> handler = InsertQueryLogEvent;
            if (handler != null)
            {
                QueryLogEventArgs args = new QueryLogEventArgs();
                args.Action = Action;
                args.StartTime = STime;
                args.EndTime = ETime;
                args.Query = Qry;
                handler(this, args);
            }
        }

        protected virtual void OnInsertQueryLogger(string Action, string Qry, DateTime STime, DateTime? ETime, string UserName)
        {
            EventHandler<QueryLogEventArgs> handler = InsertQueryLogEvent;
            if (handler != null)
            {
                QueryLogEventArgs args = new QueryLogEventArgs();
                args.Action = Action;
                args.StartTime = STime;
                args.EndTime = ETime;
                args.Query = Qry;
                args.UserName = UserName;
                handler(this, args);
            }
        }
    }
    
}


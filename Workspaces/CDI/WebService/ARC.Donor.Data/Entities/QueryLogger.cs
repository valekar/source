using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Entities
{
    public abstract class QueryLogger
    {
        protected DateTime _StartTime;
        protected DateTime? _EndTime;
        protected string _Query;
        public DateTime QueryStartTime
        {
            get { return _StartTime; }
        }

        public DateTime? QueryEndTime
        {
            get { return _EndTime; }
        }
        public string Query
        {
            get { return _Query; }
        }
    }
}

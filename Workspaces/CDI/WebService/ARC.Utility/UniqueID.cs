using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ARC.Utility
{   
    
    public static class  UniqueIDGenerator 
    {
        // reads Max+1 from DB on startup
        private static long m_NextID = 1; 

        public static long GetNextID() { return Interlocked.Increment( ref m_NextID ); }
    }
    
}
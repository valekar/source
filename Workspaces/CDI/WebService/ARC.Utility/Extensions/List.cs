using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Utility.Models
{
    public static class List
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            foreach (var cur in enumerable)
            {
                collection.Add(cur);
            }
        }
    }
}
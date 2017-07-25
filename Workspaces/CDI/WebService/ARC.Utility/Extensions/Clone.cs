using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Utility.Models
{
    public static class CloneExtensions 
    {
        public static IList<T> CloneIList<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static List<T> CloneList<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static IQueryable<T> CloneQuery<T>(this IQueryable<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()) as IQueryable<T>;
        }
    }
}
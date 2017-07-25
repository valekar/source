using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ARC.Utility
{
    
    public static class CopyObject<T> where T : class, new()
    {
        
        public static T CopyFrom<TSource>(TSource source) where TSource : class
        {
            return PropertyCopier<TSource>.Copy(source);
        }
        
        private static class PropertyCopier<TSource> where TSource : class
        {
            private static readonly Func<TSource, T> copier;
            private static readonly Exception initializationException;

            internal static T Copy(TSource source)
            {
                if (initializationException != null)
                {
                    throw initializationException;
                }
                if (source == null)
                {
                    throw new ArgumentNullException("source");
                }
                return copier(source);
            }

            static PropertyCopier()
            {
                try
                {
                    copier = BuildCopier();
                    initializationException = null;
                }
                catch (Exception e)
                {
                    copier = null;
                    initializationException = e;
                }
            }

            private static Func<TSource, T> BuildCopier()
            {
                ParameterExpression sourceParameter = Expression.Parameter(typeof(TSource), "source");
                var bindings = new List<MemberBinding>();
                foreach (PropertyInfo sourceProperty in typeof(TSource).GetProperties())
                {
                    if (!sourceProperty.CanRead)
                    {
                        continue;
                    }
                    PropertyInfo targetProperty = typeof(T).GetProperty(sourceProperty.Name);
                    if (targetProperty == null)
                    {
                        throw new ArgumentException("Property " + sourceProperty.Name + " is not present and accessible in " + typeof(T).FullName);
                    }
                    if (!targetProperty.CanWrite)
                    {
                        throw new ArgumentException("Property " + sourceProperty.Name + " is not writable in " + typeof(T).FullName);
                    }
                    if (!targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        throw new ArgumentException("Property " + sourceProperty.Name + " has an incompatible type in " + typeof(T).FullName);
                    }
                    bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParameter, sourceProperty)));
                }
                Expression initializer = Expression.MemberInit(Expression.New(typeof(T)), bindings);
                return Expression.Lambda<Func<TSource,T>>(initializer, sourceParameter).Compile();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Infrastructure
{
    public static class Extensions
    {
        public static string GetString(this IDataReader dr, string column)
        {
            var index = dr.GetOrdinal(column);
            return dr.GetString(index);
        }

        public static object GetValue(this IDataReader dr, PropertyInfo propertyInfo)
        {
            var result = dr.GetValue(
                dr.GetOrdinal(((ColumnAttribute)propertyInfo.GetCustomAttribute(typeof(ColumnAttribute))).Name)
                );

            if (propertyInfo.PropertyType == typeof(decimal))
            {

                if (result is double)
                {
                    return (decimal)(double)result;

                }
                result = (decimal)result;

            }
            if (propertyInfo.PropertyType == typeof(int))
            {
                if (result is string)
                {
                    if (result.ToString().Trim().Length == 0)
                    {
                        return string.Empty;
                    }

                }
                else
                {
                    result = Convert.ToInt32(result);
                }

            }
            if (propertyInfo.PropertyType == typeof(string) && result.GetType() != propertyInfo.PropertyType)
            {
                result = Convert.ToString(result);
            }
            if (propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (propertyInfo.PropertyType == typeof(decimal?))
                {
                    result = ChangeType<decimal>(result);
                }

            }
            //else if (propertyInfo.PropertyType == typeof(int))
            //{
            //
            //}
            //else if (propertyInfo.PropertyType == typeof(int))
            //{
            //
            //}
            //else if (propertyInfo.PropertyType == typeof(int))
            //{
            //
            //}
            //else if (propertyInfo.PropertyType == typeof(int))
            //{
            //
            //}
            //else if (propertyInfo.PropertyType == typeof(int))
            //{
            //
            //}
            return result;

        }


        public static T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }

    }
}

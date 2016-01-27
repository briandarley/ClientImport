using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Utilities;

namespace ClientImport.Infrastructure
{
    public static class Extensions
    {
        public static bool IsDate(this object value)
        {
            if (value == DBNull.Value) return false;
            var stringValue = value.ToString();
            DateTime result;

            var converted = DateTime.TryParse(stringValue, out result);

            if (!converted) return false;
            return result != DateTime.MinValue;
        }
        public static string GetString(this IDataReader dr, string column)
        {
            var index = dr.GetOrdinal(column);
            return dr.GetString(index);
        }

        public static object GetValue(this IDataReader dr, PropertyInfo propertyInfo)
        {
            try
            {
                var result = dr.GetValue(
                    dr.GetOrdinal(((ColumnAttribute)propertyInfo.GetCustomAttribute(typeof(ColumnAttribute))).Name)
                    );

                if (propertyInfo.PropertyType == typeof(decimal) && result != DBNull.Value)
                {

                    if (result is double)
                    {
                        return (decimal)(double)result;

                    }
                    result = (decimal)result;

                }
                if (propertyInfo.PropertyType == typeof(int))
                {
                    if (result is string && result != DBNull.Value)
                    {
                        if (result.ToString().Trim().Length == 0)
                        {
                            return string.Empty;
                        }
                        result = Convert.ToInt32(result);
                    }
                    else if (result != DBNull.Value)
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
                    if (propertyInfo.PropertyType == typeof(decimal?) && result != DBNull.Value)
                    {

                        result = ChangeType<decimal>(result);


                    }

                }
                if (propertyInfo.PropertyType == typeof(DateTime) && result.GetType() != propertyInfo.PropertyType)
                {
                    if (result.IsDate())
                    {
                        result = Convert.ToDateTime(result);
                    }
                    else
                    {
                        result = null;
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
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

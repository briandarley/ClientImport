using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Archived.ClientImport.Infrastructure
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
            if (dr.IsDBNull(index)) return string.Empty;
            if (dr.GetFieldType(index) == typeof(decimal))
            {
                return dr.GetDecimal(index).ToString();
            }
            return dr.GetString(index);
        }

        public static object GetValue(this IDataReader dr, PropertyInfo propertyInfo)
        {
            try
            {
                var fieldName = ((ColumnAttribute) propertyInfo.GetCustomAttribute(typeof (ColumnAttribute))).Name;
                int ordinal;

                try
                {
                    ordinal = dr.GetOrdinal(fieldName);
                }
                catch (Exception e)
                {
                    return DBNull.Value;
                }
                
                var result = dr.GetValue(ordinal);
                if (propertyInfo.PropertyType == typeof(double) && result != DBNull.Value)
                {
                    result = (double)ConvertToDecimal(result);
                }
                if (propertyInfo.PropertyType == typeof(decimal) && result != DBNull.Value)
                {
                    result = ConvertToDecimal(result);
                }
                if (propertyInfo.PropertyType == typeof(int))
                {
                    result = ConvertToInt(result);
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
                        var tmpValue = result as string;
                        if (tmpValue != null && tmpValue.Trim().Length == 0)
                        {
                            return 0m;
                        }
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

        public static DateTime ToDate(this string value)
        {
            if(string.IsNullOrEmpty(value.Trim())) return DateTime.MinValue;

            return value.Contains("/") 
                ? DateTime.Parse(value) 
                : DateTime.ParseExact(value, "MMddyyyy", CultureInfo.InvariantCulture);
        }

        public static bool ToBool(this string value)
        {
            if (value.IsEmpty()) return false;
            if (value.ToUpper() == "Y") return true;
            if (value.ToUpper() == "YES") return true;
            if (value.ToUpper() == "T") return true;
            if (value.ToUpper() == "TRUE") return true;

            return false;
        }

        private static int ConvertToInt(object result)
        {
            var stringValue = result as string;
            if (stringValue != null && result != DBNull.Value)
            {
                stringValue = Regex.Replace(stringValue, @"\.0+$", "");
                return result.ToString().Trim().Length == 0 
                    ? 0 
                    : int.Parse(stringValue);
            }
            if (result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }

        private static decimal ConvertToDecimal(object result)
        {

            if (result is double)
            {
                return (decimal)(double)result;
            }
            var tmpVal = result as string;
            if (tmpVal != null && Regex.IsMatch(tmpVal, "^0+"))
            {
                result = Regex.Replace(tmpVal, "^0+", "");
                decimal tmpResult;
                decimal.TryParse((string)result, out tmpResult);
                return tmpResult;
            }
            return (decimal)result;
        }

        public static bool IsEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            if (value.Trim().Length == 0) return true;

            return false;
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



        public static int GetInt32(this IDataReader dr, string column)
        {
            var index = dr.GetOrdinal(column);
            return dr.GetInt32(index);
        }

    }
}

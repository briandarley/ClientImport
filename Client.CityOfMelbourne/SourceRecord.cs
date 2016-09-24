using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Core;
using Core.Conversion;
using Core.Interfaces;
using Core.Infrastructure;
namespace Client.CityOfMelbourne
{
    [EntityName(Constants.Entities.CityOfMelbourne)]
    public class SourceRecord : IClientRecord
    {
        
        public string JwsCompanyId { get; set; }

        private SourceRecord() { }

        public IEnumerable<string> PropertyNames()
        {
            var t = GetType();
            var properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return properties.OrderBy(c => c.Name).Select(propertyInfo => propertyInfo.Name);
        }



        [Column("Last_Name")]
        public string LastName { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("Middle_Initial")]
        public string MiddleInitial { get; set; }
        [Column("Name_Suffix")]
        public string NameSuffix { get; set; }
        [Column("SSN")]
        public string SocialSecurityNumber { get; set; }
        [Column("Sex")]
        public string Gender { get; set; }
        [Column("Birth_Date")]
        public DateTime DateOfBirth { get; set; }
        [Column("Marital_Status")]
        public string MaritalStatus { get; set; }
        [Column("Address1")]
        public string Address1 { get; set; }
        [Column("Address2")]
        public string Address2 { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("Zip")]
        public string ZipCode { get; set; }
        [Column("Phone")]
        public string PhoneNumber { get; set; }
        [Column("Employee_ID")]
        public string EmployeeId { get; set; }
        [Column("Pay_Rate_Type")]
        public string PayRateType { get; set; }
        [Column("Pay_Rate")]
        public decimal? PayRate { get; set; }

        [Column("Hours_Worked_Per_Day")]
        public int HoursWorkedPerDay { get; set; }
        [Column("Days_Worked_Per_Week")]
        public int HoursWorkedPerWeek { get; set; }
        [Column("Hire_Date")]
        public DateTime HireDate { get; set; }
        [Column("Job_Class_Code")]
        public string JobClassCode { get; set; }
        [Column("Job_Description")]
        public string JobDescription { get; set; }
        [Column("Occupation_Code")]
        public string OccupationCode { get; set; }
        [Column("Union_Code")]
        public string UnionCode { get; set; }
        [Column("Number_Pay_Periods")]
        public int NumberOfPayPeriods { get; set; }
        [Column("Pay_Rate_per_pay_period")]
        public decimal PayRatePerPeriod { get; set; }
        [Column("Annual_Hours")]
        public int AnnualHours { get; set; }
        [Column("Annual_Pay_Rates")]
        public decimal AnnualPayRae { get; set; }
        [Column("Department Code")]
        public string DepartmentNumber { get; set; }




        public IClientRecord GetRecord(string companyId, string record)
        {
          
            var result = new SourceRecord();
            var t = typeof(SourceRecord);
            var properties = t.GetProperties().Where(c => c.GetCustomAttribute(typeof(FixedLengthAttribute)) != null);
            //int start,end;
            foreach (var propertyInfo in properties)
            {
                var fixedLengthAtr = propertyInfo.GetCustomAttribute<FixedLengthAttribute>();

                var val = record.Substring(fixedLengthAtr.Start, fixedLengthAtr.Length).Trim();
                //var propertyName = propertyInfo.Name;

                //start = fixedLengthAtr.Start;
                //end = start + fixedLengthAtr.Length;


                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(result, val);
                }
                else if (propertyInfo.PropertyType == typeof(decimal))
                {
                    decimal decimalValue;
                    var stringValue = Regex.Replace(val, @"(\d+)(\d{2}$)", "$1.$2");
                    if (decimal.TryParse(stringValue, out decimalValue))
                    {
                        propertyInfo.SetValue(result, decimalValue);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(DateTime))
                {
                    DateTime dateValue;
                    if (DateTime.TryParse(val, out dateValue))
                    {
                        propertyInfo.SetValue(result, dateValue);
                    }

                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    int intValue;
                    if (int.TryParse(val, out intValue))
                    {
                        propertyInfo.SetValue(result, intValue);
                    }

                }
                else if (propertyInfo.PropertyType == typeof(decimal?))
                {
                    decimal decimalValue;
                    if (decimal.TryParse(val, out decimalValue))
                    {
                        propertyInfo.SetValue(result, decimalValue);
                    }

                }
                
                //var value = dr.GetValue(propertyInfo);
                //if (value != DBNull.Value)
                //{
                //    propertyInfo.SetValue(result, value);
                //}


            }
            result.HoursWorkedPerWeek = 0;
            result.JwsCompanyId = companyId;


            return result;


            


        }


        public IClientRecord GetRecord(string companyId,IDataReader dr)
        {
            
            var result = new SourceRecord();
            var t = typeof(SourceRecord);
            var properties = t.GetProperties().Where(c => c.GetCustomAttribute(typeof(ColumnAttribute)) != null);
            foreach (var propertyInfo in properties)
            {
                var value = dr.GetValue(propertyInfo);

                if (value == DBNull.Value) continue;

                if (propertyInfo.PropertyType == typeof(int) && value is string)
                {
                    int convertedValue;
                    var isInt = int.TryParse(value.ToString(), out convertedValue);
                    if (isInt)
                    {
                        propertyInfo.SetValue(result, convertedValue);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(decimal) && value is string)
                {
                    decimal convertedValue;
                    var isDecimal = decimal.TryParse(value.ToString(), out convertedValue);
                    if (isDecimal)
                    {
                        propertyInfo.SetValue(result, convertedValue);
                    }
                }
                else
                {
                    propertyInfo.SetValue(result, value);
                }
            }
            result.JwsCompanyId = companyId;
            
            return result;

        }
        
        

    }
}

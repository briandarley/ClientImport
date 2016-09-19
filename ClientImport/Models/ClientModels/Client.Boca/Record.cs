using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Boca
{
    public class Record: IRecord<Record>
    {
        public string Tier1CompanyId { get; set; }
        [Column("Last Name"),FixedLength(0, 39)]
        public string LastName { get; set; }
        [Column("First Name"), FixedLength(40, 40)]
        public string FirstName { get; set; }
        [Column("Middle Initial"), FixedLength(80, 1)]
        public string MiddleInitial { get; set; }
        [Column("Name Suffix"), FixedLength(81, 4)]
        public string NameSuffix { get; set; }
        [Column("Social Security Number"), FixedLength(85,9)]
        public string SocialSecurityNumber { get; set; }
        [Column("Gender"), FixedLength(94, 1)]
        public string Gender { get; set; }
    [Column("Date of Birth"), FixedLength(95, 10)]
        public DateTime DateOfBirth { get; set; }
        [Column("Marital Status"), FixedLength(105, 1)]
        public string MaritalStatus { get; set; }
        [Column("Address Line 1"), FixedLength(106, 80)]
        public string Address1 { get; set; }
        [Column("Address Line 2")]
        public string Address2 { get; set; }
        [Column("City"), FixedLength(186, 20)]
        public string City { get; set; }
        [Column("State"), FixedLength(206, 2)]
        public string State { get; set; }
        [Column("Zip Code"), FixedLength(208, 9)]
        public string ZipCode { get; set; }
        [Column("Phone Number"), FixedLength(217, 10)]
        public string PhoneNumber { get; set; }

        [Column("Pay Rate Type"), FixedLength(227, 1)]
        public string PayRateType { get; set; }
        [Column("Pay Rate"),FixedLength(228, 9,2)]
        public decimal PayRate { get; set; }

        [Column("Hours Worked Per Day"), FixedLength(237, 4,2)]
        public decimal HoursWorkedPerDay { get; set; }

        [Column("Days worked per week"), FixedLength(241, 2)]
        public int HoursWorkedPerWeek { get; set; }

        [Column("Hire Date"), FixedLength(243, 10)]
        public DateTime HireDate { get; set; }

        [Column("Job Class Code"), FixedLength(253, 6)]
        public string JobClassCode { get; set; }

        [Column("Job Description"), FixedLength(259, 60)]
        public string JobDescription { get; set; }

        [Column("Location Code"), FixedLength(319, 11)]
        public string LocationCode { get; set; }

        [Column("Location Name(Description)"), FixedLength(330, 40)]
        public string LocationName { get; set; }

        [Column("EmployeeID"), FixedLength(370, 9)]
        public string EmployeeId { get; set; }
        [Column("Department Number"), FixedLength(379, 6)]
        public string DepartmentNumber { get; set; }

        [Column("O5Num")]
        public string Level5Number { get; set; }
        
        [Column("Division Name"), FixedLength(385, 30)]
        public string DivisionName { get; set; }
        [Column("Department Name"), FixedLength(411, 30)]
        public string DepartmentName { get; set; }

        [Column("O5Name"), FixedLength(441, 30)]
        public string Level5Name { get; set; }


        public static Record GetRecord(IDataReader dr)
        {
            var result = new Record();
            var t = typeof (Record);
            var properties = t.GetProperties().Where(c => c.GetCustomAttribute(typeof (ColumnAttribute)) != null);
            foreach (var propertyInfo in properties)
            {
                var value = dr.GetValue(propertyInfo);
                if(value != DBNull.Value)
                {
                    propertyInfo.SetValue(result, value);
                }

            
            }
            result.Tier1CompanyId = Constants.Clients.BocaCompanyNumber;
            return result;
            
        }

        public static Record GetRecord(string record)
        {
            var result = new Record();
            var t = typeof(Record);
            var properties = t.GetProperties().Where(c => c.GetCustomAttribute(typeof(FixedLengthAttribute)) != null);
            //int start,end;
            foreach (var propertyInfo in properties)
            {
                var fixedLengthAtr = propertyInfo.GetCustomAttribute<FixedLengthAttribute>();
                
                var val = record.Substring(fixedLengthAtr.Start, fixedLengthAtr.Length).Trim();
                //var propertyName = propertyInfo.Name;

                //start = fixedLengthAtr.Start;
                //end = start + fixedLengthAtr.Length;


                if (propertyInfo.PropertyType == typeof (string))
                {
                    propertyInfo.SetValue(result, val);
                }
                else if (propertyInfo.PropertyType == typeof (decimal))
                {
                    decimal decimalValue;
                    var stringValue = Regex.Replace(val, @"(\d+)(\d{2}$)", "$1.$2");
                    if (decimal.TryParse(stringValue, out decimalValue))
                    {
                        propertyInfo.SetValue(result, decimalValue);
                    }
                }
                else if (propertyInfo.PropertyType == typeof (DateTime))
                {
                    DateTime dateValue;
                    if (DateTime.TryParse(val,out dateValue))
                    {
                        propertyInfo.SetValue(result, dateValue);
                    }
                
                }
                else if (propertyInfo.PropertyType == typeof (int))
                {
                    int intValue;
                    if (int.TryParse(val, out intValue))
                    {
                        propertyInfo.SetValue(result, intValue);
                    }
                   
                }
                else if (propertyInfo.PropertyType == typeof (decimal?))
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
            
            return result;

        }

        
    }
}

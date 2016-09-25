using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;

namespace Client.SarasotaCounty
{
    [EntityName(Core.Constants.Entities.SarasotaCounty)]
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
        [Column("First_Name")]
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
        [Column("Tier1_Company_id")]
        public string Tier1Company { get; set; }
        [Column("Tier_Level")]
        public string TierLevel { get; set; }
        [Column("Tier_level_id")]
        public string TierLevelId { get; set; }
        [Column("Cost_Center_Name")]
        public string CostCenterName { get; set; }
        [Column("User_Level")]
        public string UserLevel { get; set; }
        [Column("Index_Code")]
        public string IndexCode { get; set; }
        [Column("Number_Pay_Periods")]
        public int NumberPayPeriods { get; set; }
        [Column("Pay_Rate_per_pay_period")]
        public int PayRatePerPeriod { get; set; }
        [Column("Annual_Hours")]
        public double AnnualHours { get; set; }
        [Column("Annual_Pay_Rate")]
        public double AnnualPayRate { get; set; }




        public IClientRecord GetRecord(string companyId, string record)
        {
            throw new NotSupportedException();
        }

        public IClientRecord GetRecord(string companyId, IDataReader dr)
        {
            var result = new SourceRecord();
            var t = typeof(SourceRecord);
            var properties = t.GetProperties().Where(c => c.GetCustomAttribute(typeof(ColumnAttribute)) != null);
            foreach (var propertyInfo in properties)
            {
                var value = dr.GetValue(propertyInfo);
                if (value != DBNull.Value)
                {
                    propertyInfo.SetValue(result, value);
                }


            }
            result.JwsCompanyId = companyId;
            return result;
        }




    }
}

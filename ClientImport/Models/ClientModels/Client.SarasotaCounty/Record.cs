using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.SarasotaCounty
{
    public class Record: IRecord<Record>
    {
        private string _companyNumber;
        [Column("Tier1_Company_id")]
        public string Tier1CompanyId { get; set; }
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
        //[Column("Tier1_Company_id")]
        //public string Tier1Company { get; set; }
        [Column("Tier_Level")]
        public string TierLevel{ get; set; }
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
        
        
        
        //[Column("Company Number")]
        //public string CompanyNumber
        //{
        //    get
        //    {
        //        return _companyNumber ?? Constants.Clients.SarasotaCounty;
                
        //    }
        //    set { _companyNumber = value; }
        //}

        


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
            
            return result;
            
        }

        
    }
}

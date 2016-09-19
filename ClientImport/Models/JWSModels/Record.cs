using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Core.Interfaces;

namespace ClientImport.Models.JWSModels
{
    public class Record:IRecord
    {
        [Column("Last_Name"), MaxLength(40)]
        public string LastName { get; set; }
        [Column("First_Name"), MaxLength(40)]
        public string FirstName { get; set; }
        [Column("Middle_Initial"), MaxLength(1)]
        public string MiddleInitial { get; set; }
        [Column("Name_Suffix"), MaxLength(4)]
        public string NameSuffix { get; set; }

        [Column("SSN"), MaxLength(9)]
        public string SocialSecurityNumber { get; set; }

        [Column("Sex"), MaxLength(1)]
        public string Gender { get; set; }

        [Column("Birth_Date")]
        public DateTime DateOfBirth { get; set; }

        [Column("Marital_Status"), MaxLength(1)]
        public string MaritalStatus { get; set; }

        [Column("Address1"), MaxLength(40)]
        public string AddressLine1 { get; set; }

        [Column("Address2"), MaxLength(40)]
        public string AddressLine2 { get; set; }

        [Column("City"), MaxLength(20)]
        public string City { get; set; }

        [Column("State"), MaxLength(2)]
        public string State { get; set; }

        [Column("Zip"), MaxLength(9)]
        public string ZipCode { get; set; }

        [Column("Phone"), MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Column("Employee_ID"), MaxLength(20)]
        public string EmployeeId { get; set; }
        [Column("Pay_Rate_Type"), MaxLength(1)]
        public string PayRateType { get; set; }
        [Column("Pay_Rate")]
        public decimal PayRate { get; set; }
        [Column("Hours_Worked_Per_Day")]
        public decimal HoursWorkedPerDay { get; set; }
        [Column("Days_Worked_Per_Week")]
        public decimal DaysWorkedPerWeek { get; set; }
        [Column("Hire_Date")]
        public DateTime HireDate { get; set; }
        [Column("Job_Class_Code"), MaxLength(6)]
        public string JobClassCode { get; set; }
        [Column("Job_Description"), MaxLength(60)]
        public string JobDescription { get; set; }
        [Column("Occupation_Code"), MaxLength(4)]
        public string OccupationCode { get; set; }
        [Column("Union_Code"), MaxLength(10)]
        public string UnionCode { get; set; }



        //[Column("Group Name"), MaxLength(40)]
        //public string GroupName { get; set; }
        //[Column("Group Number")]
        //public int GroupNumber{ get; set; }
        //[Column("Tier1_Company_id"), MaxLength(40)]
        //public string CompanyName { get; set; }
        [Column("Tier1_Company_id")]
        public string Tier1CompanyId { get; set; }

        [Column("Tier_Level")]
        public int TierLevel { get; set; }

        [Column("Tier_level_id")]
        public string TierLevelId { get; set; }
        [Column("Cost_Center_Name")]
        public string TierName { get; set; }
        [Column("User_Level")]
        public string UserLevel { get; set; }
        [Column("Index_Code")]
        public string IndexCode { get; set; }
        [Column("Number_Pay_Periods")]
        public int NumberPayPeriods { get; set; }
        [Column("Pay_Rate_per_pay_period")]
        public decimal PayRatePerPayPeriod { get; set; }
        [Column("Annual_Hours")]
        public double AnnualHours { get; set; }
        [Column("Annual_Pay_Rate")]
        public decimal AnnualPayRate { get; set; }

        public void NormalizeFields()
        {
            var properties = GetType().GetProperties().Where(c => c.GetCustomAttribute<MaxLengthAttribute>() != null).ToList();

            foreach (var propertyInfo in properties)
            {
                var maxLength = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
                var value = (string)propertyInfo.GetValue(this, BindingFlags.Public | BindingFlags.Instance, null, null, null);
                if (value != null && value.Length > maxLength.Length)
                {
                    value = value.Substring(0, maxLength.Length);
                    propertyInfo.SetValue(this, value);
                }


            }
        }


        public void Format()
        {
            if (TierLevel == 0)
            {
                TierLevel = 2;
            }
            if (string.IsNullOrEmpty(SocialSecurityNumber))
            {
                SocialSecurityNumber = new string('0', 9);
            }
            if (DateOfBirth == DateTime.MinValue)
            {
                DateOfBirth = new DateTime(1950, 1, 1);
            }
            if (string.IsNullOrEmpty(MaritalStatus))
            {
                MaritalStatus = "Unknown";
            }
            if (string.IsNullOrEmpty(AddressLine1))
            {
                AddressLine1 = "x";
            }
            if (string.IsNullOrEmpty(City))
            {
                City = "x";
            }
            if (string.IsNullOrEmpty(State))
            {
                State = "x";
            }
            if (string.IsNullOrEmpty(ZipCode))
            {
                ZipCode = "00000";
            }
            if (ZipCode.Contains("-"))
            {
                ZipCode= Regex.Match(ZipCode, @"^\d+").Value;
            }




            

        }
    }
}

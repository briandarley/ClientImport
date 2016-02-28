using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.MiamiJewish
{
    public class Record : IRecord<Record>
    {
        private string _socialSecurityNumber;
        public string Tier1CompanyId { get; set; }
        [Column("F2")]//Last Name
        public string LastName { get; set; }
        [Column("F3")]//First Name
        public string FirstName { get; set; }
        [Column("F4")]//Middle Name
        public string MiddleInitial { get; set; }
        [Column("F5")]//Employee Number
        public string EmployeeId { get; set; }
        [Column("F6")]//SSN (Formatted)
        public string SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }
            set { _socialSecurityNumber = value.Replace("-", ""); }
        }
        [Column("F7")]//Gender Code
        public string Gender { get; set; }
        [Column("F8")]//Date Of Birth
        public DateTime DateOfBirth { get; set; }
        [Column("F9")]//Marital Status
        public string MaritalStatus { get; set; }
        [Column("F10")]//Address Line 1
        public string Address1 { get; set; }
        [Column("F11")]//Address Line 2
        public string Address2 { get; set; }
        [Column("F12")]//City
        public string City { get; set; }
        [Column("F13")]//State/Province
        public string State { get; set; }
        [Column("F14")]//Zip Code
        public string ZipCode { get; set; }
        [Column("F16")]//Home Phone
        public string PhoneNumber { get; set; }
        [Column("F17")]//Hourly Pay Rate
        public decimal? PayRate { get; set; }
        [Column("F18")]//Annual Salary
        public decimal AnnualSallary { get; set; }
        [Column("F19")]//Scheduled Work Hours
        public double ScheduledWorkHours { get; set; }
        [Column("F20")]//Original Hire Date
        public DateTime OriginalHireDate { get; set; }
        [Column("F21")]//Last Hire Date
        public DateTime HireDate { get; set; }
        [Column("F22")]//Job Title
        public string JobDescription { get; set; }
        [Column("F23")]//Company
        public string Company { get; set; }
        [Column("F24")]//Job Code
        public string JobClassCode { get; set; }
        [Column("F25")]//Cost Center
        public string DepartmentNumber { get; set; }
        [Column("F26")]//Department
        public string DepartmentName { get; set; }
        [Column("F27")]//Div
        public int DivisionNumber { get; set; }
        [Column("F28")]//Division
        public string DivisionName { get; set; }
        [Column("F29")]//Supervisor
        public string Supervisor { get; set; }
        [Column("F30")]//Employee Type Code
        public string EmployeeTypeCode { get; set; }
        [Column("F31")]//Full/Part Time Code
        public string WorkTypeCode { get; set; }
        [Column("F32")]//Salary Or Hourly Code
        public string PayRateType { get; set; }
        [Column("F33")]//Shift
        public string Shift { get; set; }
        [Column("F34")]//Ethnicity
        public string Ethnicity { get; set; }


        public static Record GetRecord(IDataReader dr)
        {
            var result = new Record();
            var t = typeof(Record);
            var properties = t.GetProperties().Where(c => c.GetCustomAttribute(typeof(ColumnAttribute)) != null);
            foreach (var propertyInfo in properties)
            {
                var value = dr.GetValue(propertyInfo);
                if (value != DBNull.Value)
                {
                    propertyInfo.SetValue(result, value);
                }


            }
            result.Tier1CompanyId = Constants.Clients.MiamiJewishCompanyNumber;
            return result;

        }


    }
}

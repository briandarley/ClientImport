using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Boca
{
    public class Record: IRecord<Record>
    {
        [Column("Last Name")]
        public string LastName { get; set; }
        [Column("First Name")]
        public string FirstName { get; set; }
        [Column("Middle Initial")]
        public string MiddleInitial { get; set; }
        [Column("Name Suffix")]
        public string NameSuffix { get; set; }
        [Column("Social Security Number")]
        public string SocialSecurityNumber { get; set; }
        [Column("Gender")]
        public string Gender { get; set; }
        [Column("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Column("Marital Status")]
        public string MaritalStatus { get; set; }
        [Column("Address Line 1")]
        public string Address1 { get; set; }
        [Column("Address Line 2")]
        public string Address2 { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("Zip Code")]
        public string ZipCode { get; set; }
        [Column("Phone Number")]
        public string PhoneNumber { get; set; }
        [Column("Pay Rate Type")]
        public string PayRateType { get; set; }
        [Column("Pay Rate")]
        public decimal? PayRate { get; set; }
        [Column("Hours Worked Per Day")]
        public int HoursWorkedPerDay { get; set; }
        [Column("Days worked per week")]
        public int HoursWorkedPerWeek { get; set; }
        [Column("Hire Date")]
        public DateTime HireDate { get; set; }
        [Column("Job Class Code")]
        public string JobClassCode { get; set; }
        [Column("Job Description")]
        public string JobDescription { get; set; }
        [Column("Location Code")]
        public string LocationCode { get; set; }
        [Column("Location Name(Description)")]
        public string LocationName { get; set; }
        [Column("EmployeeID")]
        public string EmployeeId { get; set; }
        [Column("Department Number")]
        public string DepartmentNumber { get; set; }
        [Column("O5Num")]
        public string Level5Number { get; set; }
        [Column("Division Name")]
        public string DivisionName { get; set; }
        [Column("Department Name")]
        public string DepartmentName { get; set; }
        [Column("O5Name")]
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
            return result;
            
        }

        
    }
}

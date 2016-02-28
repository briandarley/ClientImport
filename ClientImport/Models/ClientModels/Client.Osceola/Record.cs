using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Osceola
{
    public class Record: IRecord<Record>
    {

        public string Tier1CompanyId { get; set; }
        [Column("LAST NAME")]
        public string LastName { get; set; }
        [Column("FIRST NAME")]
        public string FirstName { get; set; }
        [Column("I")]
        public string MiddleInitial { get; set; }
        [Column("SUFX")]
        public string NameSuffix { get; set; }
        [Column("ID")]
        public string EmployeeId { get; set; }
        [Column("SSN")]
        public string SocialSecurityNumber { get; set; }
        [Column("S")]
        public string Gender { get; set; }
        [Column("BIRTH DT")]
        public DateTime DateOfBirth { get; set; }
        [Column("U")]
        public string MaritalStatus { get; set; }
        [Column("ADDR1")]
        public string Address1 { get; set; }
        [Column("ADDR2")]
        public string Address2 { get; set; }
        [Column("CITY")]
        public string City { get; set; }
        [Column("ST")]
        public string State { get; set; }
        [Column("ZIPCODE")]
        public string ZipCode { get; set; }
        [Column("PHONE")]
        public string PhoneNumber { get; set; }
        [Column("HRLY RT")]
        public decimal PayRate { get; set; }
        

        [Column("HR DAY")]
        public double HoursWorkedPerDay { get; set; }
        [Column("D/W")]
        public int DaysWorkedPerWeek { get; set; }
        [Column("HIRE DT")]
        public DateTime HireDate { get; set; }
        [Column("JOB CD")]
        public string JobClassCode { get; set; }
        [Column("JOB DESC")]
        public string JobDescription { get; set; }
        [Column("LOCAT")]
        public string DivisionNumber { get; set; }
        [Column("LOCAT DESC")]
        public string DivisionName { get; set; }
        
 

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
            result.Tier1CompanyId = Constants.Clients.OsceolaCompanyNumber;
            return result;
            
        }

        
    }
}

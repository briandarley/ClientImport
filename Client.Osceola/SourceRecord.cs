using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;

namespace Client.Osceola
{
    [EntityName(Core.Constants.Entities.Osceola)]
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

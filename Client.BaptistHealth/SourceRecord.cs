using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;

namespace Client.BaptistHealth
{
    [EntityName(Core.Constants.Entities.BaptistHealth)]
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

        


        [Column("Last Name")]
        public string LastName { get; set; }
        [Column("First Name")]
        public string FirstName { get; set; }
        [Column("Middle Initial")]
        public string MiddleInitial { get; set; }
        [Column("Social Security Number")]
        public string SocialSecurityNumber { get; set; }
        [Column("Gender")]
        public string Gender { get; set; }
        [Column("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Column("Address Line 1")]
        public string Address1 { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("Zip Code")]
        public string ZipCode { get; set; }
        [Column("Hire Date")]
        public DateTime HireDate { get; set; }


        public IClientRecord GetRecord(string companyId, string record)
        {
            var columns = record.Split(new[] { "|" }, StringSplitOptions.None);

            DateTime hireDate;
            DateTime.TryParse(columns[11], out hireDate);
            if (hireDate.IsEmpty())
            {
                hireDate = DateTime.Parse("01/01/1901");
            }
            var claimant = new SourceRecord
            {
                LastName = columns[0],
                FirstName = columns[1],
                MiddleInitial = columns[2],
                SocialSecurityNumber = columns[3],
                Gender = columns[4],
                DateOfBirth = DateTime.Parse(columns[5]),
                Address1 = columns[6],
                City = columns[8],
                State = columns[9],
                ZipCode = columns[10],
                HireDate = hireDate,
                JwsCompanyId = companyId
            };

            return claimant;

        }
        public IClientRecord GetRecord(string companyId, IDataReader dr)
        {
            throw new NotSupportedException();
        }
    }
}

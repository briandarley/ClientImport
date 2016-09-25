using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Core.Conversion;
using Core.Interfaces;

namespace Client.LeeCountySb
{
    [EntityName(Core.Constants.Entities.LeeCountySb)]
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
        [Column("Address Line 1")]
        public string Address2 { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("Zip Code")]
        public string ZipCode { get; set; }
        [Column("Hire Date")]
        public DateTime HireDate { get; set; }
        [Column("Marital Status")]
        public string MaritalStatus { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNumber { get; set; }
        public decimal PayRate { get; set; }

        public string DivisionNumber { get; set; }
        public string DivisionName { get; set; }



        public IClientRecord GetRecord(string companyId, string record)
        {




            var columns = record.Split(new[] { "|" }, StringSplitOptions.None);


            var result = new SourceRecord
            {
                FirstName = columns[0],
                LastName = columns[1],
                MiddleInitial = columns[2],
                SocialSecurityNumber = columns[3],
                Gender = columns[4],
                DateOfBirth = columns[5] == "" ? DateTime.Parse("1/1/1890") : DateTime.Parse(columns[5]),
                MaritalStatus = columns[6],
                Address1 = columns[7],
                Address2 = columns[8],
                City = columns[9],
                State = columns[10],
                ZipCode = columns[11],
                EmployeeId = columns[12],
                DepartmentName = columns[13],
                DepartmentNumber = columns[14],
                //HoursWorkedPerDay = decimal.Parse(columns[15]),
                PayRate = decimal.Parse(columns[16]),

                DivisionNumber = columns[17],
                DivisionName = columns[18],
                HireDate = DateTime.Parse(columns[19])
            };

            result.JwsCompanyId = companyId;
            return result;

        }

        public IClientRecord GetRecord(string companyId, IDataReader dr)
        {
            throw new NotSupportedException();
        }




    }
}

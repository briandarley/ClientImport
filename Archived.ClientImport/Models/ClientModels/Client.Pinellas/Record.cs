using System;
using System.ComponentModel.DataAnnotations.Schema;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;

namespace Archived.ClientImport.Models.ClientModels.Client.Pinellas
{
    public class Record: IRecord<Record>
    {
        public string Tier1CompanyId { get; set; }
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
        [Column("Employee Number")]
        public string EmployeeId { get; set; }
        [Column("Job Title")]
        public string JobTitle { get; set; }
        [Column("Job Code")]
        public string JobClassCode { get; set; }

        [Column("Hours Per Day")]
        public double HoursWorkedPerDay { get; set; }
        [Column("Days Per Week")]
        public double DaysWorkedPerWeek { get; set; }

        [Column("Division Number")]
        public int DivisionNumber { get; set; }
        [Column("Pay Rate Type")]
        public string PayRateType { get; set; }

        [Column("Pay Rate")]
        public decimal PayRate { get; set; }
        
        [Column("Hire Date")]
        public DateTime HireDate { get; set; }
        
        
        

        
        internal static IRecord<Record> GetRecord(string record)
        {
            //BRUCE|RONALD|R|001343642|M|12131945|M|66151 TUDOR RD N||PINELLAS PARK|FL|33782|7277292586|BUS DRIVER|212| 8.000| 5|  473.42|02242014|5590|W

            //BRUCE|RONALD|R|001343642|M|12131945|M|66151 TUDOR RD N||PINELLAS PARK|FL|33782
            //|7277292586|BUS DRIVER|212| 8.000| 5|  473.42|02242014|5590|W
            try
            {
                var columns = record.Split(new[] {"|"}, StringSplitOptions.None);
                if (columns.Length < 17) return null;
                var claimant = new Record
                {
                    LastName = columns[0],
                    FirstName = columns[1],
                    MiddleInitial = columns[2],
                    SocialSecurityNumber = columns[3],
                    Gender = columns[4],
                    DateOfBirth = columns[5].ToDate(),
                    MaritalStatus = columns[6],
                    Address1 = columns[7],
                    Address2 = columns[8],
                    City = columns[9],
                    State = columns[10],
                    ZipCode = columns[11],
                    EmployeeId = columns[12],
                    JobTitle = columns[13],
                    JobClassCode = columns[14],
                    HoursWorkedPerDay = double.Parse(columns[15]),
                    DaysWorkedPerWeek = double.Parse(columns[16]),
                    PayRate = decimal.Parse(columns[17]),
                    HireDate = columns[18].ToDate(),
                    DivisionNumber = int.Parse(columns[19]),
                    PayRateType = columns[20]

                };
                claimant.Tier1CompanyId = Constants.Clients.PinellasCompanyNumber;
                return claimant;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            return null;
        }

        
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.BaptistHealth
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
        
        
        

        
        internal static IRecord<Record> GetRecord(string record)
        {
            //BAKER|LISA|A|265731153|F|09/09/1967|5222 Rainey Avenue East||Orange Park|FL|32065|12/21/1990
            var columns = record.Split(new[] {"|"}, StringSplitOptions.None);
            
            var claimant = new Record
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
                HireDate = DateTime.Parse(columns[11])
            };
            claimant.Tier1CompanyId = Constants.Clients.BaptistHealthCompanyNumber;
            return claimant;
        }

        
        
    }
}

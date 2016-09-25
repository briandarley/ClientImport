using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;

namespace Archived.ClientImport.Models.ClientModels.Client.Ocbocc
{
    public class Record: IRecord<Record>
    {

        public string Tier1CompanyId { get; set; }
        [Column("Last_Name")]
        public string LastName { get; set; }
        [Column("First_Name")]
        public string FirstName { get; set; }
        [Column("Middle_Initial")]
        public string MiddleInitial { get; set; }
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
        [Column("Employee_id")]
        public string EmployeeId { get; set; }


        [Column("Tier1_Company_id")]
        public string CompanyNumber { get; set; }

        [Column("Tier_Level")]
        public string TierLevel { get; set; }








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
            result.Tier1CompanyId = Constants.Clients.OcboccCompanyNumber;
            return result;
            
        }

        
    }
}

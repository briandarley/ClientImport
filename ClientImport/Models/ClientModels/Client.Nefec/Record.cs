using System;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Nefec
{
    public class Record: IRecord<Record>
    {
        private string _maritalStatus;
        public string Tier1CompanyId { get; set; }
        [Column("CENAME1")]
        public string LastName { get; set; }
        [Column("CENAME2")]
        public string FirstName { get; set; }
        [Column("CEMIDDLE")]
        public string MiddleInitial { get; set; }
        [Column("CEACCREDIT")]
        public string NameSuffix { get; set; }
        [Column("CESSNUM")]
        public string SocialSecurityNumber { get; set; }
        [Column("CESEX")]
        public string Gender { get; set; }
        [Column("CEBIRTHDT")]
        public DateTime DateOfBirth { get; set; }
        [Column("CEMARITAL")]
        public string MaritalStatus
        {
            get { return _maritalStatus; }
            set { _maritalStatus = value.Substring(1,1); }
        }

        [Column("CEADDR1")]
        public string Address1 { get; set; }
        [Column("CEADDR2")]
        public string Address2 { get; set; }
        [Column("CECITY")]
        public string City { get; set; }
        [Column("CESTATE")]
        public string State { get; set; }
        [Column("CEZIP")]
        public string ZipCode { get; set; }
        [Column("CEPHONE")]
        public string PhoneNumber { get; set; }
        [Column("CEEMPLOYEEID")]
        public string EmployeeId { get; set; }
        [Column("CEPAYRATETYPE")]
        public string PayRateType { get; set; }
        [Column("CEPAYRATE")]
        public decimal PayRate { get; set; }
        [Column("CEHRSWKPERDAY")]
        public decimal HoursPerWeek { get; set; }
        [Column("CEDAYSWKPERWK")]
        public int DaysWorkedPerWeek { get; set; }
        [Column("CEHIREDT")]
        public DateTime HireDate { get; set; }
        [Column("CEJOBCD")]
        public string JobClassCode { get; set; }
        [Column("CEJOBTITLE")]
        public string JobDescription { get; set; }
        [Column("CEOCCUPATIONCD")]
        public string OccupationCode { get; set; }
        [Column("CEUNIONCODE")]
        public string UnionCode { get; set; }
        [Column("CEGPNUM")]
        public int GroupNumber { get; set; }
        [Column("CECPNUM")]
        public int CompanyNumber { get; set; }
        
        [Column("CEDIVNUM")]
        public string DivisionNumber { get; set; }
        [Column("CEDEPTNUM")]
        public string DepartmentNumber { get; set; }
        [Column("CECOSTCENTERNAME")]
        public string CompanyName { get; set; }
        [Column("CEUSERLEVEL")]
        public string UserLevel        { get; set; }
        [Column("CEINDEXCODE")]
        public string IndexCode { get; set; }


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
            result.Tier1CompanyId = Constants.Clients.NefecCompanyNumber;
            return result;
            
        }

        
    }
}

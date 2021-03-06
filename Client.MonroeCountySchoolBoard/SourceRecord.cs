﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;

namespace Client.MonroeCountySchoolBoard
{
    [EntityName(Core.Constants.Entities.MonroeCountySchoolBoard)]
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
        public string MaritalStatus { get; set; }
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
        [Column("CEPayRateType")]
        public string PayRateType { get; set; }
        [Column("CEPayRate")]
        public decimal? PayRate { get; set; }
        [Column("CEHRSWRKPERDAY")]
        public int HoursWorkedPerDay { get; set; }
        [Column("CEHRSWRKPERWK")]
        public int HoursWorkedPerWeek { get; set; }
        [Column("CEHireDt")]
        public DateTime HireDate { get; set; }
        [Column("CEJOBCD")]
        public string JobClassCode { get; set; }
        [Column("CEJOBTITLE")]
        public string JobDescription { get; set; }
        [Column("CECOSTCENTERNAME")]
        public string DivisionName { get; set; }
        [Column("CENTER NO")]
        public string DivisionNumber { get; set; }




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

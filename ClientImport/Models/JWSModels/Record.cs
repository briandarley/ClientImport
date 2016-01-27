using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientImport.Models.JWSModels
{
    public class Record
    {
        [Column("Last Name"), MaxLength(40)]
        public string LastName { get; set; }
        [Column("First Name"), MaxLength(40)]
        public string FirstName { get; set; }
        [Column("Middle Initial"), MaxLength(1)]
        public string MiddleInitial { get; set; }
        [Column("Name Suffix"), MaxLength(4)]
        public string NameSuffix { get; set; }
        [Column("Social Security Number"), MaxLength(9)]
        public string SocialSecurityNumber { get; set; }
        [Column("Gender"), MaxLength(1)]
        public string Gender { get; set; }
        [Column("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Column("Marital Status"), MaxLength(1)]
        public string MaritalStatus { get; set; }
        [Column("Address Line 1"), MaxLength(40)]
        public string AddressLine1 { get; set; }
        [Column("Address Line 2"), MaxLength(40)]
        public string AddressLine2 { get; set; }
        [Column("City"), MaxLength(20)]
        public string City { get; set; }
        [Column("State"), MaxLength(2)]
        public string State { get; set; }
        [Column("Zip Code"), MaxLength(9)]
        public string ZipCode { get; set; }
        [Column("Phone Number"), MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Column("Employee ID"), MaxLength(20)]
        public string EmployeeId { get; set; }
        [Column("Pay Rate Type"), MaxLength(1)]
        public string PayRateType { get; set; }
        [Column("Pay Rate")]
        public decimal PayRate { get; set; }
        [Column("Hours Worked Per Day")]
        public decimal HoursWorkedPerDay { get; set; }
        [Column("Days Worked Per Week")]
        public decimal DaysWorkedPerWeek{ get; set; }
        [Column("Hire Date")]
        public DateTime HireDate { get; set; }
        [Column("Job Class Code"), MaxLength(6)]
        public string JobClassCode { get; set; }
        [Column("Job Description"), MaxLength(60)]
        public string JobDescription { get; set; }
        [Column("Occupation Code"), MaxLength(4)]
        public string OccupationCode { get; set; }
        [Column("Union Code"), MaxLength(10)]
        public string UnionCode{ get; set; }
        [Column("Group Name"), MaxLength(40)]
        public string GroupName { get; set; }
        [Column("Group Number")]
        public int GroupNumber{ get; set; }
        [Column("Company Name"), MaxLength(40)]
        public string CompanyName { get; set; }
        [Column("Company Number")]
        public int CompanyNumber{ get; set; }
        [Column("Division Number"), MaxLength(11)]
        public string DivisionNumber { get; set; }
        [Column("Division Name"), MaxLength(10)]
        public string DivisionName{ get; set; }
        [Column("Department Number"), MaxLength(11)]
        public string DepartmentNumber{ get; set; }
        [Column("Department Name"), MaxLength(40)]
        public string DepartmentName { get; set; }
        [Column("Level 5 Number"), MaxLength(11)]
        public string Level5Number { get; set; }
        [Column("Level 5 Name"), MaxLength(40)]
        public string Level5Name { get; set; }
        [Column("Level 6 Number"), MaxLength(11)]
        public string Level6Number { get; set; }
        [Column("Level 6 Name"), MaxLength(40)]
        public string Level6Name { get; set; }
        [Column("Level 7 Number"), MaxLength(11)]
        public string Level7Number { get; set; }
        [Column("Level 7 Name"), MaxLength(40)]
        public string Level7Name { get; set; }

        

    }
}

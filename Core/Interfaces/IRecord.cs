using System;

namespace Core.Interfaces
{
    public interface IRecord
    {
        
        string LastName { get; set; }
        string FirstName { get; set; }
        string MiddleInitial { get; set; }
        string NameSuffix { get; set; }
        string SocialSecurityNumber { get; set; }
        string Gender { get; set; }
        DateTime DateOfBirth { get; set; }
        string MaritalStatus { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
        string PhoneNumber { get; set; }
        string EmployeeId { get; set; }
        string PayRateType { get; set; }
        decimal PayRate { get; set; }
        decimal HoursWorkedPerDay { get; set; }
        decimal DaysWorkedPerWeek { get; set; }
        DateTime HireDate { get; set; }
        string JobClassCode { get; set; }
        string JobDescription { get; set; }
        string OccupationCode { get; set; }
        string UnionCode { get; set; }
        string Tier1CompanyId { get; set; }
        int TierLevel { get; set; }
        string TierLevelId { get; set; }
        string TierName { get; set; }
        string UserLevel { get; set; }
        string IndexCode { get; set; }
        int NumberPayPeriods { get; set; }
        decimal PayRatePerPayPeriod { get; set; }
        double AnnualHours { get; set; }
        decimal AnnualPayRate { get; set; }

        void Format();
        void NormalizeFields();
    }
}

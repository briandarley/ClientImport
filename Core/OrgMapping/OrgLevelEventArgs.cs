using System;
using Core.JwsModels;

namespace Core.OrgMapping
{
    public class OrgLevelEventArgs : EventArgs
    {
        public Record Record { get; set; }
        public string CompanyId { get; set; }
        public string DivisonNumber { get; set; }
        public string DepartmentNumber { get; set; }
        public string Level5Number { get; set; }
        public string Name { get; set; }
        public string CostCenterName { get; set; }
        public string DivisionName { get; set; }
        public string DivisionNumber { get; set; }
        public string Level5Name { get; set; }
        public string Level6Number { get; set; }
        public string Level6Name { get; set; }

        public OrgLevelEventArgs(string companyId, Record record)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new ArgumentException("Company Id is Required");
            }
            CompanyId = companyId;
            Record = record;
        }
    }
}

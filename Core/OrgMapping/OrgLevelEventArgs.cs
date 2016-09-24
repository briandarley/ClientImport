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
        public string O5Level { get; set; }
        public string Name { get; set; }
        public string CostCenterName { get; set; }

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

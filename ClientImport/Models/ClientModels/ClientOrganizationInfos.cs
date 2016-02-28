using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Models.ClientModels
{
    public class ClientOrganizationInfos
    {
        private List<ClientOrganizationInfo> _organizationInfos;

        public List<ClientOrganizationInfo> OrganizationInfos
        {
            get { return _organizationInfos ?? (_organizationInfos = new List<ClientOrganizationInfo>()); }
            set { _organizationInfos = value; }
        }

        public ClientOrganizationInfos()
        {
            OrganizationInfos = new List<ClientOrganizationInfo>();
        }

        public void Add(ClientOrganizationInfo info)
        {
           OrganizationInfos.Add(info);
        }

      

        public bool Any(Func<ClientOrganizationInfo, bool> func)
        {
            return OrganizationInfos != null && OrganizationInfos.Any(func);
        }
        public bool Any()
        {
            return OrganizationInfos != null && OrganizationInfos.Count > 0;
        }

        public override string ToString()
        {
            if (OrganizationInfos == null || !OrganizationInfos.Any()) return string.Empty;
            var sb = new StringBuilder();
            sb.Append("LEVEL\t\tName\t\t\t\t\t\t\t\t\tParent Name");
            sb.Append("\n");
            foreach (var clientOrganizationInfo in OrganizationInfos)
            {
                sb.Append(clientOrganizationInfo);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}

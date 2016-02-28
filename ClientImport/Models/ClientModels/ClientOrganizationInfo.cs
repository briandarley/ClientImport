using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Models.ClientModels
{
    public class ClientOrganizationInfo
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            
            var tabsTotal = (int)Math.Ceiling((decimal)Name.Length/4);
            var tabsRemaining = 11 - tabsTotal;
            var tabstring = new string('\t', tabsRemaining);

            sb.Append($"{Level}\t\t\t{Name}{tabstring}{ParentName}");
            return sb.ToString();
        }
    }
}

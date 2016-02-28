using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Models.ClientModels
{
    public class ClientLogEventArgs: EventArgs
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string ParentName { get; set; }
    }
}

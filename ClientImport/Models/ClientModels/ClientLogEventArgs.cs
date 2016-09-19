using System;

namespace ClientImport.Models.ClientModels
{
    public class ClientLogEventArgs: EventArgs
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string ParentName { get; set; }
    }
}

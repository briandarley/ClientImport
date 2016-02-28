using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Models.ClientModels
{
    public interface IModelBuilder
    {
        event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;
    }
}

using System;

namespace Archived.ClientImport.Models.ClientModels
{
    public interface IModelBuilder
    {
        event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;
    }
}

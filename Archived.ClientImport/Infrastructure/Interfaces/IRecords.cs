using System.Collections.Generic;
using System.IO;
using Archived.ClientImport.Models.ClientModels;

namespace Archived.ClientImport.Infrastructure.Interfaces
{
    public interface IRecords<T> where T : new()
    {
        string ClientName { get; set; }
        ClientOrganizationInfos MultipleOrganizationMappings { get; set; }
        ClientOrganizationInfos MissingOrganizationMappings { get; set; }

        void OnMissingOrganizationMappingEncountered(object sender, ClientLogEventArgs e);
        void OnMultipleOrganizationMappingEncountered(object sender, ClientLogEventArgs e);

        IEnumerable<FileInfo> FileSystemFiles { get; set; }
        List<IRecord<T>> Records { get; set; }

        int ConvertSourceContents();
        void ArchiveSourceFile();
    }
}

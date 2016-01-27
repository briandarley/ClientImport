using System.Collections.Generic;
using System.IO;

namespace ClientImport.Infrastructure.Interfaces
{
    public interface IRecords<T> where T : new()
    {
        IEnumerable<FileInfo> FileSystemFiles { get; set; }
        List<IRecord<T>> Records { get; set; }

        //void ReadSourceFileRecords();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;
using Archived.ClientImport.Models.JWSModels;


namespace Archived.ClientImport.Models.ClientModels
{
    public abstract class BaseRepository<T> : IRecords<T> where T : new()
    {
        private readonly string _sourcePath;
        private readonly string _fileExtension;
        public string ClientName { get; set; }
        public abstract ClientOrganizationInfos MultipleOrganizationMappings { get; set; }
        public abstract ClientOrganizationInfos MissingOrganizationMappings { get; set; }
        public abstract void OnMissingOrganizationMappingEncountered(object sender, ClientLogEventArgs e);
        public abstract void OnMultipleOrganizationMappingEncountered(object sender, ClientLogEventArgs e);
        public abstract string Tier2NullValue { get; set; }
        public IEnumerable<FileInfo> FileSystemFiles { get; set; }
        public List<IRecord<T>> Records { get; set; }

        List<IRecord<T>> IRecords<T>.Records { get; set; }


        protected Logger _logger;
        protected BaseRepository(string clientName, string sourcePath, string fileExtension)
        {
            _sourcePath = sourcePath;
            _fileExtension = fileExtension;
            ClientName = clientName;
            _logger = new Logger();
        }

        protected abstract List<Record> ConvertClientData(IEnumerable<IRecord<T>> records);

        private IEnumerable<IEnumerable<IRecord<T>>> GetAllRecords()
        {
            foreach (var fileInfo in FileSystemFiles)
            {
                _logger.LogGetRecordsForGivenPath(ClientName, fileInfo.FullName);
                IEnumerable<IRecord<T>> records = ReadSourceFileRecords(fileInfo.FullName);
                yield return records;

            }
        }

        protected abstract IEnumerable<IRecord<T>> ReadSourceFileRecords(string filePath);

        public int ConvertSourceContents()
        {
            _logger.InitializingProcess(ClientName);
            FindAllFilesFromSourcePath();

            var allRecords = GetAllRecords().ToList();
            var totalRecords = allRecords.Where(c => c != null).Sum(c => c.Count());
            _logger.TotalFilesIdentified(totalRecords);

            if (totalRecords == 0)
            {
                _logger.NoRecordsToProcessForClient(ClientName);
                return 0;
            }
            _logger.ConvertingFileContentsFor(ClientName);


            foreach (var fileContents in allRecords)
            {
                try
                {
                    if (fileContents == null) continue;

                    var records = ConvertClientData(fileContents);
                    foreach (var record in records)
                    {
                        if (record == null) continue;
                        record.Format();
                    
                        //todo should TierLevel '2' be set to null?
                        //if (record.TierLevel == 2)
                        //{
                        //    record.TierLevelId = Tier2NullValue;
                        //}
                    }

                    var repo = new Repository { Records = records.ToList() };


                    var basePath = $@"{Constants.BaseDestinationPath}\{ClientName}\";
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    var fileName = "OUT_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");
                    var outputPath = Path.Combine(basePath, fileName + ".xlsx");
                    if (File.Exists(outputPath))
                    {
                        File.Delete(outputPath);
                    }
                    repo.WriteRecordsToExcelFile(outputPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }





            return totalRecords;
        }
        protected void FindAllFilesFromSourcePath()
        {
            _logger.FindAllFilesFromSourcePath(ClientName);
            FileSystemFiles = new DirectoryInfo(_sourcePath)
                .GetFiles()
                .Where(c => c.Extension.ToUpper() == "." + _fileExtension.ToUpper())
                .Where(c => !c.Name.Contains("~$"));
        }
        public void ArchiveSourceFile()
        {
            _logger.ArchivingSourceFIles(ClientName);
            var basePath = $@"{Constants.BaseDestinationPath}\{ClientName}\";


            foreach (var fileSystemFile in FileSystemFiles)
            {
                var fileName = "SRC_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff") + "." + fileSystemFile.Extension;

                fileSystemFile.MoveTo(Path.Combine(basePath, fileName));
            }
        }
    }
}

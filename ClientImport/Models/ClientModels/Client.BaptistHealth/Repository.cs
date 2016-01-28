using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.BaptistHealth
{
    public class Repository : IRecords<Record>
    {
        public IEnumerable<FileInfo> FileSystemFiles { get; set; }

        private readonly Logger _logger;

        public List<IRecord<Record>> Records { get; set; }

        public Repository()
        {
            _logger = new Logger();
        }

        private void FindAllFilesFromSourcePath()
        {
            _logger.FindAllFilesFromSourcePath(Constants.Clients.BaptistHealth);
            FileSystemFiles = new DirectoryInfo(Constants.ConfigBaptistHealthFileSource)
                .GetFiles()
                .Where(c => c.Extension.ToUpper() == "." + Constants.ConfigBaptistHealthFileExt.ToUpper())
                .Where(c => !c.Name.Contains("~$"));
        }

        private IEnumerable<IEnumerable<IRecord<Record>>> GetAllRecords()
        {
            foreach (var fileInfo in FileSystemFiles)
            {
                _logger.LogGetRecordsForGivenPath(Constants.Clients.BaptistHealth, fileInfo.FullName);
                IEnumerable<IRecord<Record>> records = ReadSourceFileRecords(fileInfo.FullName);
                yield return records;

            }
        }

        public void ConvertSourceContents()
        {
            _logger.InitializingProcess(Constants.Clients.BaptistHealth);
            FindAllFilesFromSourcePath();


            var allRecords = GetAllRecords().ToList();
            var totalRecords = allRecords.Where(c => c != null).Sum(c => c.Count());
            _logger.TotalFilesIdentified(totalRecords);

            if (totalRecords == 0)
            {
                _logger.NoRecordsToProcessForClient(Constants.Clients.BaptistHealth);
                return;
            }
            _logger.ConvertingFileContentsFor(Constants.Clients.BaptistHealth);



            foreach (var fileContents in allRecords)
            {
                var records = ConvertClientData(fileContents);
                var repo = new JWSModels.Repository() { Records = records };

                var outputPath = Path.Combine(Constants.DestinationDirectory, Constants.Clients.BaptistHealth + ".xlsx");
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                repo.WriteRecordsToExcelFile(outputPath);
            }
        }

        private List<JWSModels.Record> ConvertClientData(IEnumerable<IRecord<Record>> records)
        {

            var modelBuilder = new ModelBuilder();
            return modelBuilder.GetJwsRecordsFromClientRecords(records);

        }



        private IEnumerable<IRecord<Record>> ReadSourceFileRecords(string filePath)
        {
            try
            {
                var contents = File.ReadAllText(filePath);
                var records = contents.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                var result = records.Select(Record.GetRecord).ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Clients.BaptistHealth, ex);

            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.CityOfMelbourne
{
    public class Repository : IRecords<Record>
    {
        public IEnumerable<FileInfo> FileSystemFiles { get; set; }
        private readonly string _filePath;
        private Logger _logger;

        public List<IRecord<Record>> Records { get; set; }

        public Repository()
        {
            _filePath = Constants.ConfigCityOfMelbourneFileSource;
            _logger = new Logger();
        }

        private void FindAllFilesFromSourcePath()
        {
            _logger.FindAllFilesFromSourcePath(Constants.Clients.CityOfMelbourne);
            FileSystemFiles = new DirectoryInfo(Constants.ConfigCityOfMelbourneFileSource)
                .GetFiles()
                .Where(c => c.Extension.ToUpper() == "." + Constants.ConfigCityOfMelbourneFileExt.ToUpper())
                .Where(c => !c.Name.Contains("~$"));
        }

        private IEnumerable<IEnumerable<IRecord<Record>>> GetAllRecords()
        {
            foreach (var fileInfo in FileSystemFiles)
            {
                _logger.LogGetRecordsForGivenPath(Constants.Clients.CityOfMelbourne, fileInfo.FullName);
                IEnumerable<IRecord<Record>> records = ReadSourceFileRecords(fileInfo.FullName);
                yield return records;

            }
        }

        public void ConvertSourceContents()
        {
            _logger.InitializingProcess(Constants.Clients.CityOfMelbourne);
            FindAllFilesFromSourcePath();

            var allRecords = GetAllRecords().ToList();
            var totalRecords = allRecords.Where(c => c != null).Sum(c => c.Count());
            _logger.TotalFilesIdentified(totalRecords);

            if (totalRecords == 0)
            {
                _logger.NoRecordsToProcessForClient(Constants.Clients.CityOfMelbourne);
                return;
            }
            _logger.ConvertingFileContentsFor(Constants.Clients.CityOfMelbourne);




            foreach (var fileContents in allRecords)
            {
                var records = ConvertClientData(fileContents);
                var repo = new JWSModels.Repository() { Records = records };


                var outputPath = Path.Combine(Constants.DestinationDirectory, Constants.Clients.CityOfMelbourne + ".xlsx");
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
                var connectionString = string.Format(Constants.ExcelConnectionString, filePath);
                using (var cn = new OleDbConnection(connectionString))
                {
                    cn.Open();
                    var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string sheet1 = schema.Rows[0].Field<string>("TABLE_NAME");
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = $"select * from [{sheet1}]";
                    var dr = cmd.ExecuteReader();

                    var result = new List<IRecord<Record>>();


                    var rownum = 0;
                    while (dr.Read())
                    {
                        rownum++;

                        if (rownum == 1)
                        {
                            continue;
                        }

                        result.Add(Record.GetRecord(dr));

                    }
                    return result;


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Clients.CityOfMelbourne, ex);

            }
            return null;
        }
    }
}

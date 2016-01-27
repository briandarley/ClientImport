using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Nefec
{
    public class Repository : IRecords<Record>
    {
        public IEnumerable<FileInfo> FileSystemFiles { get; set; }
        private readonly string _filePath;
        private Logger _logger;

        public List<IRecord<Record>> Records { get; set; }

        public Repository()
        {
            _filePath = Constants.ConfigNefecFileSource;
            _logger = new Logger();
        }

        private void FindAllFilesFromSourcePath()
        {
            _logger.FindAllFilesFromSourcePath(Constants.Clients.Nefec);
            FileSystemFiles = new DirectoryInfo(Constants.ConfigNefecFileSource)
                .GetFiles()
                .Where(c => c.Extension.ToUpper() == "." + Constants.ConfigNefecFileExt.ToUpper())
                .Where(c => !c.Name.Contains("~$"));
        }

        private IEnumerable<IEnumerable<IRecord<Record>>> GetAllRecords()
        {
            foreach (var fileInfo in FileSystemFiles)
            {
                _logger.LogGetRecordsForGivenPath(Constants.Clients.Nefec, fileInfo.FullName);
                IEnumerable<IRecord<Record>> records = ReadSourceFileRecords(fileInfo.FullName);
                yield return records;

            }
        }

        public void ConvertSourceContents()
        {
            _logger.InitializingProcess(Constants.Clients.Nefec);
            FindAllFilesFromSourcePath();

            var allRecords = GetAllRecords().ToList();
            _logger.TotalFilesIdentified(allRecords.Where(c=> c!= null).Sum(c=>c.Count()));

            _logger.ConvertingFileContentsFor(Constants.Clients.Nefec);

            foreach (var fileContents in allRecords)
            {
                var records = ConvertClientData(fileContents);
                var repo = new JWSModels.Repository(){Records = records};
                var outputPath = Path.Combine(Constants.DestinationDirectory, Constants.Clients.Nefec + ".xlsx");
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
                    if (schema != null)
                    {
                        string sheet1 = schema.Rows[0].Field<string>("TABLE_NAME");
                        var cmd = cn.CreateCommand();
                        cmd.CommandText = $"select * from [{sheet1}]";
                        var dr = cmd.ExecuteReader();

                        var result = new List<IRecord<Nefec.Record>>();


                        var rownum = 0;
                        while (dr.Read())
                        {
                            rownum++;

                            if (rownum == 1)
                            {
                                continue;
                            }

                            result.Add(Nefec.Record.GetRecord(dr));

                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Clients.Nefec, ex);

            }
            return null;
        }
    }
}

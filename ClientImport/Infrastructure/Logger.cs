using System;
using log4net;

namespace ClientImport.Infrastructure
{
    public class Logger
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Logger));

        
        

        public void LogRecordsRetrieved(int records)
        {
            _logger.Debug($"Reading processed from source file {records}");
        }

        public void LogError(string client, Exception ex)
        {
            _logger.Debug($"Exception occurred processing {client}, details {ex}");
        }

        /// <summary>
        /// Called when attempting to locate all file system records for the given client
        /// </summary>
        /// <param name="client"></param>
        public void FindAllFilesFromSourcePath(string client)
        {
            _logger.Debug($"Locating all files for {client}");
        }
        /// <summary>
        /// Called each time a source file is processed
        /// </summary>
        /// <param name="client"></param>
        /// <param name="filePath"></param>
        public void LogGetRecordsForGivenPath(string client, string filePath)
        {
            _logger.Debug($"Reading contents for {client} from {filePath}");
        }

        public void InitializingProcess(string client)
        {
            _logger.Debug($"Initializing process for client {client}.\nSteps include\n1. Read source folder for all files.\n2.Iterate through each file and process it\n3.Convert the contents to JWS format.\n4.Write out the contents to a new Excel file.");
        }

        public void ConvertingFileContentsFor(string client)
        {
            _logger.Debug($"Preparing to convert file contents for {client} to JWS format");
        }

        public void TotalFilesIdentified(int count)
        {
            _logger.Debug($"Total files identified for conversion {count}");
        }

        public void NoRecordsToProcessForClient(string client)
        {
            _logger.Debug($"No records to process for client {client}.");
        }

        public void ArchivingSourceFIles(string client)
        {
            _logger.Debug($"Archiving records for {client}.");
        }

        public void LogGeneralMessage(string message)
        {
            _logger.Debug(message);
        }
    }
}

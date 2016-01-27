using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientImport.Infrastructure
{
    public class Constants
    {
        public static string DestinationDirectory = System.Configuration.ConfigurationManager.AppSettings["file-destination"];
        /// <summary>
        /// Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"
        /// </summary>
        public const string ExcelConnectionString  = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";


        public static string ConfigBocaFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:boca"];
        public static string ConfigBocaFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:boca"];

        public static string ConfigBaptistHealthFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:baptistHealth"];
        public static string ConfigBaptistHealthFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:baptistHealth"];

        public static string ConfigCityOfMelbourneFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:cityOfMelbourne"];
        public static string ConfigCityOfMelbourneFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:cityOfMelbourne"];

        public static string ConfigLeeCountySbFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:leeCounty"];
        public static string ConfigLeeCountySbFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:leeCounty"];

        public class Clients
        {
            
            public static string Boca = "Boca";
            public static string BocaFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:boca"];
            public static string BocaCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:boca"];
            
            
            public static string BaptistHealth = "BaptistHealth";
            public static string BaptistHealthFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:baptistHealth"];
            public static string BaptistHealthCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:baptistHealth"];


            public static string CityOfMelbourne = "CityOfMelbourne";
            public static string CityOfMelbourneFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:cityOfMelbourne"];
            public static string CityOfMelbourneCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:cityOfMelbourne"];


            public static string LeeCountySchoolBoard = "LeeCountySB";
            public static string LeeCountySchoolBoardFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:leeCountySB"];
            public static string LeeCountySchoolBoardCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:leeCountySB"];

        }

    }
}

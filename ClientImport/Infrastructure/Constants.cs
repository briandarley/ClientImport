namespace ClientImport.Infrastructure
{
    public class Constants
    {
        public static string DestinationDirectory = System.Configuration.ConfigurationManager.AppSettings["file-destination"];
        /// <summary>
        /// Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"
        /// </summary>
        public const string ExcelConnectionString  = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";

        /// <summary>
        /// Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;\"
        /// </summary>
        /// <returns>
        /// http://stackoverflow.com/questions/1139390/excel-external-table-is-not-in-the-expected-format
        /// </returns>
        public const string ExcelConnectionStringLegacyVersion = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;\"";

        public static string ConfigBocaFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:boca"];
        public static string ConfigBocaFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:boca"];

        public static string ConfigBaptistHealthFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:baptistHealth"];
        public static string ConfigBaptistHealthFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:baptistHealth"];

        public static string ConfigCityOfMelbourneFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:cityOfMelbourne"];
        public static string ConfigCityOfMelbourneFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:cityOfMelbourne"];

        public static string ConfigLeeCountySbFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:leeCounty"];
        public static string ConfigLeeCountySbFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:leeCounty"];

        public static string ConfigMiamiJewishFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:miamiJewish"];
        public static string ConfigMiamiJewishFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:miamiJewish"];

        public static string ConfigMonroeCountySchoolBoardFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:monroeCountySchoolBoard"];
        public static string ConfigMonroeCountySchoolBoardFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:monroeCountySchoolBoard"];

        public static string ConfigNefecFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:nefec"];
        public static string ConfigNefecFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:nefec"];

        public static string ConfigOcboccFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:Ocbocc"];
        public static string ConfigOcboccFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:Ocbocc"];

        public static string ConfigOsceolaFileSource = System.Configuration.ConfigurationManager.AppSettings["file-source:osceola"];
        public static string ConfigOsceolaFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:osceola"];

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

            public static string MiamiJewish = "MiamiJewish";
            public static string MiamiJewishFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:miamiJewish"];
            public static string MiamiJewishCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:miamiJewish"];

            public static string MonroeCountySchoolBoard = "Monroe County School Board";
            public static string MonroeCountySchoolBoardFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:monroeCountySchoolBoard"];
            public static string MonroeCountySchoolBoardCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:monroeCountySchoolBoard"];

            public static string Nefec = "NEFEC";
            public static string NefecFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:nefec"];
            public static string NefecCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:nefec"];

            public static string Ocbocc = "OCBOCC";
            public static string OcboccFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:ocbocc"];
            public static string OcboccCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:ocbocc"];

            public static string Osceola = "Osceola";
            public static string OsceolaFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:osceola"];
            public static string OsceolaCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:osceola"];

        }



    }
}

namespace ClientImport.Infrastructure
{
    public class Constants
    {
        public static bool ArchiveSourceFiles = System.Configuration.ConfigurationManager.AppSettings["archive_source_files"].ToBool();
        public static string BaseSourcePath = System.Configuration.ConfigurationManager.AppSettings["file-source-base-path"];
        public static string BaseDestinationPath = System.Configuration.ConfigurationManager.AppSettings["file-destination-base-path"];
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

        public static string ConfigBocaFileSource = BaseSourcePath  + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:boca"];
        public static string ConfigBocaFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:boca"];
        public static bool ConfigBocaEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:boca"].ToBool();

        public static string ConfigBaptistHealthFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:baptistHealth"];
        public static string ConfigBaptistHealthFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:baptistHealth"];
        public static bool ConfigBaptistHealthEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:baptistHealth"].ToBool();


        public static string ConfigCityOfMelbourneFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:cityOfMelbourne"];
        public static string ConfigCityOfMelbourneFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:cityOfMelbourne"];
        public static bool ConfigCityOfMelbourneEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:cityOfMelbourne"].ToBool();


        public static string ConfigLeeCountySbFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:leeCounty"];
        public static string ConfigLeeCountySbFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:leeCounty"];
        public static bool ConfigLeeCountySbEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:leeCounty"].ToBool();

        public static string ConfigMiamiJewishFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:miamiJewish"];
        public static string ConfigMiamiJewishFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:miamiJewish"];
        public static bool ConfigMiamiJewishEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:miamiJewish"].ToBool();

        public static string ConfigMonroeCountySchoolBoardFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:monroeCountySchoolBoard"];
        public static string ConfigMonroeCountySchoolBoardFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:monroeCountySchoolBoard"];
        public static bool ConfigMonroeCountySchoolBoardEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:monroeCountySchoolBoard"].ToBool();

        public static string ConfigNefecFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:nefec"];
        public static string ConfigNefecFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:nefec"];
        public static bool ConfigNefecEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:nefec"].ToBool();

        public static string ConfigOcboccFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:Ocbocc"];
        public static string ConfigOcboccFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:Ocbocc"];
        public static bool ConfigOcboccEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:Ocbocc"].ToBool();

        public static string ConfigOsceolaFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:osceola"];
        public static string ConfigOsceolaFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:osceola"];
        public static bool ConfigOsceolaEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:osceola"].ToBool();

        public static string ConfigPinellasFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:pinellas"];
        public static string ConfigPinellasFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:pinellas"];
        public static bool ConfigPinellasEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:pinellas"].ToBool();

        public static string ConfigPolkCountySchoolBoardFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:polkCountySchoolBoard"];
        public static string ConfigPolkCountySchoolBoardFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:polkCountySchoolBoard"];
        public static bool ConfigPolkCountySchoolBoardEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:polkCountySchoolBoard"].ToBool();

        public static string ConfigSarasotaCountyFileSource = BaseSourcePath + "/" + System.Configuration.ConfigurationManager.AppSettings["file-source:sarasotaCounty"];
        public static string ConfigSarasotaCountyFileExt = System.Configuration.ConfigurationManager.AppSettings["file-extension:sarasotaCounty"];
        public static bool ConfigSarasotaCountyEnabled = System.Configuration.ConfigurationManager.AppSettings["file-enabled:sarasotaCounty"].ToBool();


        public class Clients
        {
            
            public static string Boca = "Boca";
            public static string BocaCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:boca"];
            
            
            public static string BaptistHealth = "BaptistHealth";
            public static string BaptistHealthCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:baptistHealth"];


            public static string CityOfMelbourne = "CityOfMelbourne";
            public static string CityOfMelbourneFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:cityOfMelbourne"];
            public static string CityOfMelbourneCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:cityOfMelbourne"];


            public static string LeeCountySchoolBoard = "LeeCountySB";
            public static string LeeCountySchoolBoardFullName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:leeCounty"];
            public static string LeeCountySchoolBoardCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:leeCounty"];

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

            public static string Pinellas = "Pinellas";
            public static string PinellasName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:pinellas"];
            public static string PinellasCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:pinellas"];


            public static string PolkCountySchoolBoard = "Polk County School Board";
            public static string PolkCountySchoolBoardName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:polkCountySchoolBoard"];
            public static string PolkCountySchoolBoardCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:polkCountySchoolBoard"];


            public static string SarasotaCounty = "Sarasota County";
            public static string SarasotaCountyName => System.Configuration.ConfigurationManager.AppSettings["file-company-name:sarasotaCounty"];
            public static string SarasotaCountyCompanyNumber => System.Configuration.ConfigurationManager.AppSettings["file-company-number:sarasotaCounty"];


        }

        public class Resources
        {
            public static string SmtpServer => System.Configuration.ConfigurationManager.AppSettings["smtp:server"];
            public static string SmtpUserId => System.Configuration.ConfigurationManager.AppSettings["smtp:userid"];
            public static string SmtpPassword => System.Configuration.ConfigurationManager.AppSettings["smtp:password"];
            public static string SmtpTo => System.Configuration.ConfigurationManager.AppSettings["smtp:to"];
            public static string SmtpFrom => System.Configuration.ConfigurationManager.AppSettings["smtp:from"];
            public static string SmtpPort => System.Configuration.ConfigurationManager.AppSettings["smtp:port"];
            
        }

        
    }
}

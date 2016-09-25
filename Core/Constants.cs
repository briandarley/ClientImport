namespace Core
{
    public class Constants
    {
        public static string BaseDestinationPath = System.Configuration.ConfigurationManager.AppSettings["file-destination-base-path"];

        public const string CsvConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Text;HDR=YES;FMT=Delimited;\"";

        public const string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";

        public class Entities
        {
            

            /// <summary>
            /// BOCA
            /// </summary>
            public const string Boca = "BOCA";
            /// <summary>
            /// BAPTIST_HEALTH
            /// </summary>
            public const string BaptistHealth = "BAPTIST_HEALTH";
            /// <summary>
            /// PINELLAS
            /// </summary>
            public const string PinellasCounty = "PINELLAS";
            /// <summary>
            /// CITY_OF_MELBOURNE
            /// </summary>
            public const string CityOfMelbourne = "CITY_OF_MELBOURNE";
            /// <summary>
            /// LEE_COUNTY_SB
            /// </summary>
            public const string LeeCountySb = "LEE_COUNTY_SB";
            /// <summary>
            /// MIAMI_JEWISH
            /// </summary>
            public const string MiamiJewish = "MIAMI_JEWISH";
            /// <summary>
            /// MONROE_COUNTY_SCHOOL_BOARD
            /// </summary>
            public const string MonroeCountySchoolBoard = "MONROE_COUNTY_SB";

            /// <summary>
            /// NEFEC
            /// </summary>
            public const string NEFEC = "NEFEC";
            /// <summary>
            /// OCBOCC
            /// </summary>
            public const string OCBOCC = "OCBOCC";
            /// <summary>
            /// OSCEOLA
            /// </summary>
            public const string Osceola = "OSCEOLA";
            /// <summary>
            /// POLK_COUNTY_SB
            /// </summary>
            public const string PolkCountySchoolBoard = "POLK_COUNTY_SB";
            /// <summary>
            /// SARASOTA_COUNTY
            /// </summary>
            public const string SarasotaCounty = "SARASOTA_COUNTY";
        }


    }
}

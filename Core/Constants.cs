namespace Core
{
    public class Constants
    {
        public static string BaseDestinationPath = System.Configuration.ConfigurationManager.AppSettings["file-destination-base-path"];

        public const string CsvConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Text;HDR=YES;FMT=Delimited;\"";

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

        }

    }
}

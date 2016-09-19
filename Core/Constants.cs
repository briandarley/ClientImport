namespace Core
{
    public class Constants
    {
        public static string BaseDestinationPath = System.Configuration.ConfigurationManager.AppSettings["file-destination-base-path"];
        public class CompanyNumbers
        {
            public static string PinellasCounty = System.Configuration.ConfigurationManager.AppSettings["file-company-number:pinellas"];
            public static string BaptistHealth = System.Configuration.ConfigurationManager.AppSettings["file-company-number:baptistHealth"];
            public static string Boca = System.Configuration.ConfigurationManager.AppSettings["file-company-number:boca"];
        }
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
        }

    }
}

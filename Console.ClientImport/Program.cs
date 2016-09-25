using ClientConversionService;

namespace Console.ClientImport
{
    class Program
    {
        
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            var service = new JwsConverterService(Core.Constants.Entities.SarasotaCounty);
            service.ConverClientFile();

            //var propertyManager = new EntityInfoPropertyManager();
            //propertyManager.CreateEntityInfoForClient();
            


        }

      
    }
}

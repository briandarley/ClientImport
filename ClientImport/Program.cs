
using log4net.Config;

namespace ClientImport
{
    class Program
    {
        static void Main(string[] args)
        {
            

            XmlConfigurator.Configure();


            //Process_Boca();
            //Process_BaptistHealth();
            //Process_CityOfMelbourne();
            Process_LeeCountSchoolBoard();

        }

        static void Process_Boca()
        {
            var repo = new Models.ClientModels.Client.Boca.Repository();
            repo.ConvertSourceContents();
        }

        static void Process_BaptistHealth()
        {
            var repo = new Models.ClientModels.Client.BaptistHealth.Repository();
            repo.ConvertSourceContents();
        }

        static void Process_CityOfMelbourne()
        {
            var repo = new Models.ClientModels.Client.CityOfMelbourne.Repository();
            repo.ConvertSourceContents();
        }

        static void Process_LeeCountSchoolBoard()
        {
            var repo = new Models.ClientModels.Client.LeeCountySb.Repository();
            repo.ConvertSourceContents();
        }




    }
}


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
            //Process_LeeCountSchoolBoard();
            //Process_MiamiJewish();
            //Process_MonroeCountySchoolBoard();
            //Process_Nefec();
            //Process_Ocbcc();
            Process_Osceola();
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

        static void Process_MiamiJewish()
        {
            var repo = new Models.ClientModels.Client.MiamiJewish.Repository();
            repo.ConvertSourceContents();
        }
        static void Process_MonroeCountySchoolBoard()
        {
            var repo = new Models.ClientModels.Client.MonroeCountySchoolBoard.Repository();
            repo.ConvertSourceContents();
        }

        static void Process_Nefec()
        {
            var repo = new Models.ClientModels.Client.Nefec.Repository();
            repo.ConvertSourceContents();
        }

        static void Process_Ocbcc()
        {
            var repo = new Models.ClientModels.Client.Ocbocc.Repository();
            repo.ConvertSourceContents();
        }

        static void Process_Osceola()
        {
            var repo = new Models.ClientModels.Client.Osceola.Repository();
            repo.ConvertSourceContents();
        }

    }
}

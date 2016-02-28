using System;
using System.Collections.Generic;
using System.Linq;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Messaging;
using ClientImport.Models.ClientModels;
using log4net.Config;

namespace ClientImport
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var logger = new Logger();
            var runList = new List<KeyValuePair<string, Action>>
            {

                new KeyValuePair<string, Action>(Constants.Clients.Boca ,Process_Boca),
                new KeyValuePair<string, Action>(Constants.Clients.BaptistHealth, Process_BaptistHealth),
                new KeyValuePair<string, Action>(Constants.Clients.CityOfMelbourne ,Process_CityOfMelbourne),
                new KeyValuePair<string, Action>(Constants.Clients.LeeCountySchoolBoard ,Process_LeeCountSchoolBoard),
                new KeyValuePair<string, Action>(Constants.Clients.MiamiJewish ,Process_MiamiJewish),
                new KeyValuePair<string, Action>(Constants.Clients.MonroeCountySchoolBoard ,Process_MonroeCountySchoolBoard),
                new KeyValuePair<string, Action>(Constants.Clients.Nefec ,Process_Nefec),
                new KeyValuePair<string, Action>(Constants.Clients.Ocbocc ,Process_Ocbcc),
                new KeyValuePair<string, Action>(Constants.Clients.Osceola ,Process_Osceola),
                new KeyValuePair<string, Action>(Constants.Clients.Pinellas ,Process_Pinellas),
                new KeyValuePair<string, Action>(Constants.Clients.PolkCountySchoolBoard ,Process_PolkCountySchoolBoard),
                new KeyValuePair<string, Action>(Constants.Clients.SarasotaCounty ,Process_SarasotaCounty        )
            };
            foreach (var action in runList)
            {
                try
                {
                    action.Value();
                }
                catch (Exception ex)
                {
                    logger.LogError(action.Key, ex);
                    throw;
                }
            }



        }



        static void Process_Boca()
        {
            if (!Constants.ConfigBocaEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage("Skipping Bocca, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.Boca.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            //var missingMappingsReport = repo.MissingOrganizationMappings.ToString();
            //var multiplMappingsReport = repo.MultipleOrganizationMappings.ToString();
            //var values = new[] { Constants.Clients.Boca, missingMappingsReport };
            GenerateReport(new[] { Constants.Clients.Boca }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }



        static void Process_BaptistHealth()
        {
            if (!Constants.ConfigBaptistHealthEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.BaptistHealth}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.BaptistHealth.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;

            GenerateReport(new[] { Constants.Clients.BaptistHealth }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_CityOfMelbourne()
        {
            if (!Constants.ConfigCityOfMelbourneEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.CityOfMelbourne}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.CityOfMelbourne.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.CityOfMelbourne }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_LeeCountSchoolBoard()
        {
            if (!Constants.ConfigLeeCountySbEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.LeeCountySchoolBoard}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.LeeCountySb.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.LeeCountySchoolBoardFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_MiamiJewish()
        {
            if (!Constants.ConfigMiamiJewishEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.MiamiJewish}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.MiamiJewish.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.LeeCountySchoolBoardFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_MonroeCountySchoolBoard()
        {
            if (!Constants.ConfigMonroeCountySchoolBoardEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.MonroeCountySchoolBoard}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.MonroeCountySchoolBoard.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.MonroeCountySchoolBoard }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_Nefec()
        {
            if (!Constants.ConfigNefecEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.Nefec}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.Nefec.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.NefecFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_Ocbcc()
        {
            if (!Constants.ConfigOcboccEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.Ocbocc}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.Ocbocc.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.OcboccFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        static void Process_Osceola()
        {
            if (!Constants.ConfigOsceolaEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.Osceola}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.Osceola.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.OsceolaFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        private static void Process_Pinellas()
        {
            if (!Constants.ConfigPinellasEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.Pinellas}, Client Process Disabled");
                return;
            }
            var repo = new Models.ClientModels.Client.Pinellas.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.PinellasName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        private static void Process_PolkCountySchoolBoard()
        {
            if (!Constants.ConfigPolkCountySchoolBoardEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.PolkCountySchoolBoard}, Client Process Disabled");
                return;
            }

            var repo = new Models.ClientModels.Client.PolkCountySchoolBoard.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.PolkCountySchoolBoardName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }
        private static void Process_SarasotaCounty()
        {
            if (!Constants.ConfigSarasotaCountyEnabled)
            {
                var logger = new Logger();
                logger.LogGeneralMessage($"Skipping {Constants.Clients.SarasotaCounty}, Client Process Disabled");
                return;
            }

            var repo = new Models.ClientModels.Client.SarasotaCounty.Repository();
            var totalRecords = repo.ConvertSourceContents();
            if (totalRecords == 0) return;
            GenerateReport(new[] { Constants.Clients.SarasotaCountyName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }

        private static void GenerateReport(string[] clients, ClientOrganizationInfos missingMappingsReport, ClientOrganizationInfos multiplMappingsReport)
        {
            var sendSuccessNotice = true;
            var mailManager = new MailManager(Constants.Resources.SmtpFrom, Constants.Resources.SmtpServer,
                    Constants.Resources.SmtpUserId, Constants.Resources.SmtpPassword);

            if (missingMappingsReport.Any())
            {
                sendSuccessNotice = false;
                missingMappingsReport.OrganizationInfos = missingMappingsReport.OrganizationInfos.OrderBy(c => c.Level).ThenBy(c => c.Name).ToList();
                mailManager.SendMailFromTemplate(MailManager.MailTemplateTypes.MissingOrganizations, Constants.Resources.SmtpTo,
                    "Failed!", new { Clients = clients, Report = missingMappingsReport });


            }
            if (multiplMappingsReport.Any())
            {
                sendSuccessNotice = false;
                missingMappingsReport.OrganizationInfos = missingMappingsReport.OrganizationInfos.OrderBy(c => c.Level).ThenBy(c => c.Name).ToList();
                mailManager.SendMailFromTemplate(MailManager.MailTemplateTypes.MultipleOrganizatonMatches, Constants.Resources.SmtpTo,
                   "Failed!", new { Clients = clients, Report = multiplMappingsReport });
            }

            if (sendSuccessNotice)
            {

                mailManager.SendMailFromTemplate(MailManager.MailTemplateTypes.Success, Constants.Resources.SmtpTo,
                    "Success!", new { Clients = clients });
            }
        }




















        //private static void Test_ReadTiers(string companyId)
        //{
        //    var organization = new Organization(companyId);
        //    Console.WriteLine(organization.Tiers.Count);

        //}
    }
}


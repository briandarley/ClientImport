﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Messaging;
using Archived.ClientImport.Models.ClientModels;
using Data.EntityInformation.Models;
using Data.EntityInformation.Repositories;
using log4net.Config;

namespace Archived.ClientImport
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            var service = new JwsConverterService(Core.Constants.Entities.SarasotaCounty);
            service.ConverClientFile();
            //CreateEntityInfoForClient();
            
         
        }

        private static void CreateEntityInfoForClient()
        {
            var repo = new EntityInfoRepository();
            repo.AddEntityConfiguration(new EntityConfiguration
            {
                EntityCode = Core.Constants.Entities.SarasotaCounty,
                CompanyNumber = "SC0100",
                Enabled = true,
                FileExtension = "xls",
                SourceFilePath = "Sarasota County"
            });
            repo.Save();


        }



        static void OLDMain(string[] args)
        {
            //ImportClientOrganization();
            //return;
            XmlConfigurator.Configure();
            var logger = new Logger();
            var runList = new List<KeyValuePair<string, Action>>
            {

                new KeyValuePair<string, Action>(Constants.Clients.Boca ,Process_Boca),
                //new KeyValuePair<string, Action>(Constants.Clients.BaptistHealth, Process_BaptistHealth),
                //new KeyValuePair<string, Action>(Constants.Clients.CityOfMelbourne ,Process_CityOfMelbourne),
                //new KeyValuePair<string, Action>(Constants.Clients.LeeCountySchoolBoard ,Process_LeeCountSchoolBoard),
                //new KeyValuePair<string, Action>(Constants.Clients.MiamiJewish ,Process_MiamiJewish),
                //new KeyValuePair<string, Action>(Constants.Clients.MonroeCountySchoolBoard ,Process_MonroeCountySchoolBoard),
                //new KeyValuePair<string, Action>(Constants.Clients.Nefec ,Process_Nefec),
                //new KeyValuePair<string, Action>(Constants.Clients.Ocbocc ,Process_Ocbcc),
                //new KeyValuePair<string, Action>(Constants.Clients.Osceola ,Process_Osceola),
                //new KeyValuePair<string, Action>(Constants.Clients.Pinellas ,Process_Pinellas),
                //new KeyValuePair<string, Action>(Constants.Clients.PolkCountySchoolBoard ,Process_PolkCountySchoolBoard),
                //new KeyValuePair<string, Action>(Constants.Clients.SarasotaCounty ,Process_SarasotaCounty        )


                //new KeyValuePair<string, Action>(Constants.Clients.BaptistHealth, Process_BaptistHealth),
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
                    logger.LogGeneralMessage("Stack Trace");
                    logger.LogGeneralMessage(ex.StackTrace);
                    throw;
                }
            }



        }

        private static void ImportClientOrganizationFromSql()
        {
            var appContext = new AppContext();


        }
        private static void ImportClientOrganizationFromExel()
        {
            var appContext = new AppContext();
            var list = appContext.OrgLevels.ToList();
            try
            {
                var file = @"C:\Users\bdarl_000\Downloads\Department Number lookup.xlsx";

                var connectionString = string.Format(Constants.ExcelConnectionString, file);
                using (var cn = new OleDbConnection(connectionString))
                {
                    cn.Open();
                    var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string sheet1 = schema.Rows[0].Field<string>("TABLE_NAME");
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = $"select * from [{sheet1}]";
                    var dr = cmd.ExecuteReader();

                    //var result = new List<IRecord<Record>>();


                    var rownum = 0;
                    while (dr.Read())
                    {
                        rownum++;

                        if (rownum == 1)
                        {
                            continue;
                        }
                        var org = appContext.OrgLevels.Create();
                        org.CompanyNumber = "000043";
                        org.Level = 3;
                        org.Name = dr.GetValue(0).ToString();
                        org.Number = dr.GetValue(1).ToString();
                        org.TierId = dr.GetValue(2).ToString();
                        appContext.OrgLevels.Add(org);



                    }



                }
                appContext.SaveChanges();
            }
            catch (Exception ex)
            {


            }

            Console.ReadLine();

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
            GenerateEmailReport(new[] { Constants.Clients.Boca }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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

            GenerateEmailReport(new[] { Constants.Clients.BaptistHealth }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.CityOfMelbourne }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            //GenerateEmailReport(new[] { Constants.Clients.LeeCountySchoolBoardFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            GenerateTextReport(new[] { Constants.Clients.LeeCountySchoolBoardFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }

        private static void GenerateTextReport(string[] clients, ClientOrganizationInfos missingMappingsReport, ClientOrganizationInfos multipleOrganizationMappings)
        {

            try
            {
                if (missingMappingsReport.Any())
                {

                    missingMappingsReport.OrganizationInfos = missingMappingsReport.OrganizationInfos.OrderBy(c => c.Level).ThenBy(c => c.Name).ToList();
                    var model = new { Report = missingMappingsReport, Clients = clients };
                    var body = MailManager.GenerateEmailBody(MailManager.MailTemplateTypes.MissingOrganizations, "Failed", model);
                    using (var sw = new StreamWriter(@"c:\temp\missingReport.txt"))
                    {
                        sw.Write(body);
                        sw.Flush();
                    }


                }
                if (multipleOrganizationMappings.Any())
                {

                    missingMappingsReport.OrganizationInfos = missingMappingsReport.OrganizationInfos.OrderBy(c => c.Level).ThenBy(c => c.Name).ToList();
                    var model = new { Report = missingMappingsReport, Clients = clients };
                    var body = MailManager.GenerateEmailBody(MailManager.MailTemplateTypes.MultipleOrganizatonMatches, "Failed", model);
                    using (var sw = new StreamWriter(@"c:\temp\multipleOrganizationMappings.txt"))
                    {
                        sw.Write(body);
                        sw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


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
            GenerateEmailReport(new[] { Constants.Clients.LeeCountySchoolBoardFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.MonroeCountySchoolBoard }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.NefecFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.OcboccFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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


            GenerateLoaclFileReport(Constants.Clients.Osceola, new[] { Constants.Clients.OsceolaFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);

            GenerateEmailReport(new[] { Constants.Clients.OsceolaFullName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.PinellasName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.PolkCountySchoolBoardName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
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
            GenerateEmailReport(new[] { Constants.Clients.SarasotaCountyName }, repo.MissingOrganizationMappings, repo.MultipleOrganizationMappings);
            if (!Constants.ArchiveSourceFiles) return;
            repo.ArchiveSourceFile();
        }

        private static void GenerateLoaclFileReport(string clientName, string[] clients, ClientOrganizationInfos missingMappingsReport, ClientOrganizationInfos multiplMappingsReport)
        {
            var basePath = $@"{Constants.BaseDestinationPath}\{clientName}\";
            var sb = new StringBuilder();


            if (missingMappingsReport.Any())
            {
                var filePath = basePath + "Missing Mapping Report.txt";
                sb.Append("Clients:\n");
                foreach (var client in clients)
                {
                    sb.Append(client + "\n");
                }
                sb.Append("\n\n");
                missingMappingsReport.OrganizationInfos = missingMappingsReport.OrganizationInfos
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.Name).ToList();

                sb.AppendFormat("{0, -10}{1, -50}{2,-50}", "Level", "Organization Name", "Parent Name");
                foreach (var clientOrganizationInfo in missingMappingsReport.OrganizationInfos)
                {
                    if (clientOrganizationInfo.Level > 3)
                    {
                        sb.AppendFormat("{0, -10}{1, -50}{2,-50}", clientOrganizationInfo.Level, clientOrganizationInfo.Name, clientOrganizationInfo.ParentName);
                    }
                    else
                    {
                        sb.AppendFormat("{0, -10}{1, -50}", clientOrganizationInfo.Level, clientOrganizationInfo.Name);
                    }
                    sb.Append("\n");
                }
                File.WriteAllText(filePath, sb.ToString());

            }
            if (multiplMappingsReport.Any())
            {

                missingMappingsReport.OrganizationInfos = missingMappingsReport.OrganizationInfos.OrderBy(c => c.Level).ThenBy(c => c.Name).ToList();

            }


        }
        private static void GenerateEmailReport(string[] clients, ClientOrganizationInfos missingMappingsReport, ClientOrganizationInfos multiplMappingsReport)
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


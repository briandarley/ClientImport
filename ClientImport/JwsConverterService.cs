using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using Core.Conversion;
using Data.EntityInformation.Repositories;
using System.ComponentModel.Composition.Hosting;
using System.Text;
using Core.Interfaces;
using Data.EntityInformation;
using log4net.Config;

namespace ClientImport
{
    public class JwsConverterService
    {
        public event EventHandler NoFilesAvailableToProcess; 
        private readonly string _entityCode;
        private EntityInfoRepository _repoEntity;
        


        public JwsConverterService(string entityCode)
        {
            _entityCode = entityCode;
            _repoEntity = new EntityInfoRepository();
        }

        public void ConverClientFile()
        {
            var converter = GetConverterByEntityCode(_entityCode);
            if (converter == null)
            {
                throw new ArgumentException($"Entity Code '{_entityCode}' is not referenced. Please reference the assembly and rebuild project.");
            }
            var configuration = _repoEntity.GetEntityConfigurationByCode(_entityCode);

            var sourceFiles = Constants.BaseSourcePath + @"\" + configuration.SourceFilePath;
            var files = Directory.GetFiles(sourceFiles, "*." + configuration.FileExtension)
                        .Where(c=>!c.StartsWith("~")).ToList();

            if (!files.Any())
            {
                NoFilesAvailableToProcess?.Invoke(this, EventArgs.Empty);
                return;
            }
            foreach (var file in files)
            {
                
                var records = converter.GetClientRecords(file).ToList();

                var list = records
                            .Select(clientRecord => converter.GetJwsRecord(clientRecord))
                            .ToList()
                            .Where(c => c != null).ToList();

                foreach (var record in list)
                {
                    record.Format();
                }
                
                var outputPath = converter.CreateOuputFile(list);
                var count = list.Count;
                LogNewlyCreatedFile(outputPath, count);
                LogMissingMappings(converter.MissingMappings);
            }

        }

        private void LogMissingMappings(List<IInvalidOrgLevel> missingMappings)
        {
            var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            XmlConfigurator.Configure();
            if (missingMappings == null)
            {
                log.Info("No Missing Mappings Detected");
            }
            else
            {
                log.Warn("The following mappings were missed");

                var sb = new StringBuilder();


                foreach (var missingMapping in missingMappings)
                {
                    sb.Append(missingMapping);
                    sb.Append("\n");
                }
                log.Warn(sb.ToString());
            }
        }

        private void LogNewlyCreatedFile(string outputPath, int count)
        {
            var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            XmlConfigurator.Configure();
            log.Info($"Total Records Processed : {count}");
            log.Info($"Newly created file '{outputPath}'");

        }

        private BaseJwsConverter GetConverterByEntityCode(string entityCode)
        {
            //Hack to load referenced assemblies if they haven't already been called
            var catalog = new DirectoryCatalog(".", "Client.*.dll");

            Func<Assembly, bool> predicate = a =>
            {
                return FindDerivedTypes(a, typeof(BaseJwsConverter))
                    .FirstOrDefault(c => c.GetCustomAttribute<EntityNameAttribute>() != null
                                         &&
                                         c.GetCustomAttribute<EntityNameAttribute>().Name.ToUpper() ==
                                         entityCode.ToUpper()) != null;
            };
            Func<Assembly, Type> jwsConverter = a =>
            {
                return FindDerivedTypes(a, typeof(BaseJwsConverter))
                    .FirstOrDefault(c => c.GetCustomAttribute<EntityNameAttribute>() != null
                                         &&
                                         c.GetCustomAttribute<EntityNameAttribute>().Name.ToUpper() ==
                                         entityCode.ToUpper());

            };
            Func<Assembly, Type> sourceRecord = a =>
            {
                return FindDerivedTypes(a, typeof(IClientRecord))
                    .FirstOrDefault(c => c.GetCustomAttribute<EntityNameAttribute>() != null
                                         &&
                                         c.GetCustomAttribute<EntityNameAttribute>().Name.ToUpper() ==
                                         entityCode.ToUpper());

            };
            var jwsConverterType = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(predicate)
                .Select(jwsConverter)
                .FirstOrDefault();

            var clintSourceType = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(predicate)
                .Select(sourceRecord)
                .FirstOrDefault();

            if (jwsConverterType == null || clintSourceType == null) return null;

            

            var sourceFileInstance = (IClientRecord) Activator.CreateInstance(clintSourceType,true);
            var jwsConverterInstance = (BaseJwsConverter)Activator.CreateInstance(jwsConverterType, sourceFileInstance);

            var entityConfiguration = ClientEntityConfiguration.Instance().GetConfigurationByEntityCode(entityCode);
            
            jwsConverterInstance.EntityConfiguration = entityConfiguration;
            jwsConverterType.GetProperty("SkipFirstLine", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(jwsConverterInstance, entityConfiguration.SkipFirstLine, null);
            
            return jwsConverterInstance;
        }

        private IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(c => c != baseType && baseType.IsAssignableFrom(c));
        }

    }
}

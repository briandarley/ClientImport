using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClientImport.Infrastructure;
using Core.Conversion;
using Data.EntityInformation.Repositories;
using System.ComponentModel.Composition.Hosting;
using log4net.Config;

namespace ClientImport
{
    public class JwsConverterService
    {
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
            var configuration = _repoEntity.GetEntityConfigurationByCode(_entityCode);

            var sourceFiles = Constants.BaseSourcePath + @"\" + configuration.SourceFilePath;
            var files = Directory.GetFiles(sourceFiles, "*." + configuration.FileExtension);

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
            Func<Assembly, Type> converter = a =>
            {
                return FindDerivedTypes(a, typeof(BaseJwsConverter))
                    .FirstOrDefault(c => c.GetCustomAttribute<EntityNameAttribute>() != null
                                         &&
                                         c.GetCustomAttribute<EntityNameAttribute>().Name.ToUpper() ==
                                         entityCode.ToUpper());

            };
            var targetType = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(predicate)
                .Select(converter)
                .FirstOrDefault();

            if (targetType == null) return null;

            var instance = (BaseJwsConverter)Activator.CreateInstance(targetType);

            return instance;
        }

        private IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(c => c != baseType && baseType.IsAssignableFrom(c));
        }

    }
}

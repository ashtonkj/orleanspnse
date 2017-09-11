using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace OrleansSilo.Configuration
{
    /// <summary>
    /// The config object for the orleans silo
    /// </summary>
    public static class Config
    {
        private static readonly IConfigurationRoot m_ConfigurationRoot;

        private static int GetFieldOrDefault(string fieldName, int defaultValue)
        {
            var value = defaultValue;
            var field = m_ConfigurationRoot[fieldName];
            if (int.TryParse(field, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            m_ConfigurationRoot = builder.Build();
        }

        /// <summary>
        /// The silo connection string
        /// </summary>
        public static string SiloConnectionString { get { return m_ConfigurationRoot.GetConnectionString("SiloConnectionString"); } }

        /// <summary>
        /// The silo port
        /// </summary>
        public static int OrleansSiloPort { get { return GetFieldOrDefault("OrleansSiloPort", 10300); } }

        /// <summary>
        /// The silo gateway port
        /// </summary>
        public static int OrleansSiloGatewayPort { get { return GetFieldOrDefault("OrleansSiloGatewayPort", 10400); } }

        /// <summary>
        /// The silo deployment id
        /// </summary>
        public static string OrleansSiloDeployment { get { return m_ConfigurationRoot["OrleansSiloDeployment"]; } }
    }
}

using System;
using System.Net;
using Orleans.Runtime.Configuration;
using OrleansSilo.Configuration;

namespace OrleansSilo
{
    /// <summary>
    /// The entry point class
    /// </summary>
    public class Program
    {
        private static OrleansHostWrapper hostWrapper;

        /// <summary>
        /// The program entry point
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            int exitCode = InitializeOrleans();
            var response = "";

            Console.WriteLine("Press Enter to terminate...");
            while (response != "Quit")
            {
                
                response = Console.ReadLine();
            }

            exitCode += ShutdownSilo();
            

            return exitCode;
        }

        private static int InitializeOrleans()
        {
            var config = new ClusterConfiguration();
            config.Globals.DataConnectionString = Config.SiloConnectionString;
            config.Globals.DeploymentId = Config.OrleansSiloDeployment;
            // TODO: This bit should be configured to use whatever our chosen persistance providers are.
            config.Globals.LivenessType = GlobalConfiguration.LivenessProviderType.SqlServer;
            config.Globals.ReminderServiceType = GlobalConfiguration.ReminderServiceProviderType.SqlServer;
            config.Defaults.PropagateActivityId = true;
            config.Defaults.ProxyGatewayEndpoint = new IPEndPoint(IPAddress.Any, Config.OrleansSiloGatewayPort);
            config.Defaults.Port = Config.OrleansSiloPort;
            config.Defaults.HostNameOrIPAddress = Dns.GetHostName();
            hostWrapper = new OrleansHostWrapper(config);
            return hostWrapper.Run();
        }

        private static int ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                return hostWrapper.Stop();
            }
            return 0;
        }
    }
}
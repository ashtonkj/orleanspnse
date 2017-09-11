using System;
using System.Net;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace OrleansSilo
{
    /// <summary>
    /// The Orleans Host Wrapper Class
    /// </summary>
    public class OrleansHostWrapper
    {
        private readonly SiloHost siloHost;

        /// <summary>
        /// The constructor for the Orleans Host Wrapper
        /// </summary>
        /// <param name="config"></param>
        public OrleansHostWrapper(ClusterConfiguration config)
        {
            siloHost = new SiloHost(Dns.GetHostName(), config);
            siloHost.LoadOrleansConfig();
        }

        /// <summary>
        /// Starts the Orleans Silo
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            if (siloHost == null)
            {
                return 1;
            }

            try
            {
                siloHost.InitializeOrleansSilo();

                if (siloHost.StartOrleansSilo())
                {
                    Console.WriteLine($"Successfully started Orleans silo '{siloHost.Name}' as a {siloHost.Type} node.");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Orleans Silo Failed to start");
                    throw new OrleansException($"Failed to start Orleans silo '{siloHost.Name}' as a {siloHost.Type} node.");
                }
            }
            catch (Exception exc)
            {
                siloHost.ReportStartupError(exc);
                Console.Error.WriteLine(exc);
                return 1;
            }
        }

        /// <summary>
        /// Stops the Orleans Silo
        /// </summary>
        /// <returns></returns>
        public int Stop()
        {
            if (siloHost != null)
            {
                try
                {
                    siloHost.StopOrleansSilo();
                    siloHost.Dispose();
                    Console.WriteLine($"Orleans silo '{siloHost.Name}' shutdown.");
                }
                catch (Exception exc)
                {
                    siloHost.ReportStartupError(exc);
                    Console.Error.WriteLine(exc);
                    return 1;
                }
            }
            return 0;
        }
    }
}

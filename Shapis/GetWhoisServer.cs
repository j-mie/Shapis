using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shapis
{
    /// <summary>
    /// Class used for looking up a WHOIS server for a given domain
    /// </summary>
    class GetWhoisServer
    {
        /// <summary>
        /// Get embedded resource http://www.vcskicks.com/embedded-resource.php
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }
        /// <summary>
        /// Get embedded resource http://www.vcskicks.com/embedded-resource.php
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(assembly, resourceName);
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    return null;

                using (StreamReader reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static Dictionary<string, string> getLookupTable()
        {
            var dict = new Dictionary<string, string>();
            var file = GetEmbeddedResource("tld_serv_list", Assembly.GetExecutingAssembly());
            
            string[] lines = Regex.Split(file, "\r\n");

            foreach (string line in lines)
            {
                try
                {
                    var tld = line.Split('\t')[0];
                    var server = line.Split('\t')[1];
                    Console.WriteLine(tld + ":" + server);
                }
                catch
                {
                }

            }
            return dict;
        }

        /// <summary>
        /// Gets the TLD for a domain
        /// </summary>
        /// <param name="domain">Domain name</param>
        /// <returns></returns>
        public string GetTLD(string domain)
        {
            getLookupTable();
            Console.ReadLine();
            var tld = string.Empty;

            if (!string.IsNullOrEmpty(domain))
            {
                var bits = domain.Split('.');
                if (bits.Length > 1) tld = bits[bits.Length - 1];
            }

            return tld;
        }

        /// <summary>
        /// Lookup the WHOIS server for a given domain.
        /// </summary>
        /// <param name="domain">Domain name</param>
        /// <returns></returns>
        public string GetServer(string domain)
        {
            var server = "whois.internic.net";
            var tld = GetTLD(domain);

            if (tld == "tk")
            {
                server = "whois.dot.tk";
            }
            else if (!string.IsNullOrEmpty(tld))
            {
                var whoisServerName = tld + '.' + "whois-servers.net";

                try
                {
                    var hostEntry = Dns.GetHostEntry(whoisServerName);

                    server = hostEntry.HostName == whoisServerName ? "whois.internic.net" : hostEntry.HostName;
                }
                catch (SocketException ex)
                {
                    // You should throw an application exception really.
                    throw new ApplicationException("WHOIS server lookup failed for " + domain + ": " + ex.Message);
                }
            }
            
            return server;
        }
    }
}

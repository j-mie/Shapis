using System;
using System.Net;
using System.Net.Sockets;

namespace Shapis
{
    /// <summary>
    /// Class used for looking up a WHOIS server for a given domain
    /// </summary>
    class GetWhoisServer
    {
        /// <summary>
        /// Gets the TLD for a domain
        /// </summary>
        /// <param name="domain">Domain name</param>
        /// <returns></returns>
        public string GetTLD(string domain)
        {
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

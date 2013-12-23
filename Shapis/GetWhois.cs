namespace Shapis
{
    /// <summary>
    /// Get a WHOIS for a given domain.
    /// </summary>
    class GetWhois
    {
        private WhoisRecord getServer(WhoisRecord record)
        {
            var serverGetter = new GetWhoisServer();
            record.Server = serverGetter.GetServer(record.Domain);
            return record;
        }

        /// <summary>
        /// Get information on a particular domain
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public WhoisRecord FillRecord(WhoisRecord record)
        {
            record.Server = getServer(record).Server;
            var tcpClient = new TCPClient();
            record.Text = tcpClient.Read(record.Server, 43, record.Domain);

            return record;
        }
    }
}

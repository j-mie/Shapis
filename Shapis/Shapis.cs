namespace Shapis
{
    /// <summary>
    /// Look up WHOIS information for a domain.
    /// </summary>
    public static class WhoisLookup
    {
        /// <summary>
        /// Looks up the WHOIS infomration for the domain.
        /// </summary>
        /// <param name="domian">Domain Name</param>
        /// <returns></returns>
        public static WhoisRecord Lookup(string domian)
        {
            var rec = new WhoisRecord {Domain = domian};

            var get = new GetWhois();
            get.FillRecord(rec);
            return rec;
        }
    }
}

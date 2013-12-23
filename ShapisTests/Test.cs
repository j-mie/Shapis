using System;
using Shapis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapisTests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void LookupExampleDomain()
        {
            var rec = WhoisLookup.Lookup("jamiehankins.co.uk");
            Console.WriteLine(rec.Domain);
            Console.WriteLine(rec.Server);
            foreach (var v in rec.Text)
            {
                Console.WriteLine(v);
            }
            Assert.AreNotEqual(true, String.IsNullOrEmpty(rec.Text.ToString()));
        }
    }
}

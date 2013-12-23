using System;
using Shapis;
using NUnit.Framework;

namespace ShapisTests
{
    [TestFixture]
    public class Test
    {
        [Test]
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

using System;
using Shapis;

namespace ShapisConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            
            if (args.Length != 0)
            {
                WhoisRecord rec = WhoisLookup.Lookup(args[0]);

                foreach (var v in rec.Text)
                {
                    Console.WriteLine(v);
                }
            }
            else
            {
                Console.WriteLine("Please add a domain as an argument!");
            }
        }
    }
}

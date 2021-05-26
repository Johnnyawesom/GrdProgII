using System;
using System.IO;
using System.Text;

namespace TextCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string iText;
            string lPath;
            string sPath;
            string[] sent;
            string[] sect;
            
            Console.WriteLine("Please choose an option:\n[l] Load text file\n[s] Divide by sentence (.)\n[p] Divide by section (,)\n[q] Quit");
            switch(Console.ReadLine())
            {
                case "l":
                    Console.WriteLine("Please provide the path to the file you wish to process:");
                    lPath = Console.ReadLine();
                    break;
                case "s":
                    //
                    break;
                case "p":
                    //
                    break;
                case "q":
                    Environment.Exit(0);
                    break;

            }
        }
    }
}

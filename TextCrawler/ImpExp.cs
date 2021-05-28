using System;
using System.IO; // Indlæsning 
using System.Collections.Generic; // Collections
using System.Text; // Encoding

namespace TextCrawler
{
    static class ImpExp
    {

        #region Gem og Læs metode
        public void Gem()
        {
            // Try-Catch i tilfælde af at ovennævnte sti ikke kan skrives til
           
        }

        public static void Laes()
        {
            string sti = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.txt");
            Console.WriteLine(sti);
            try
            {
                string[] tekst = File.ReadAllLines(sti, Encoding.UTF8);
                foreach (string s in tekst)
                {
                    Console.WriteLine(s);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Tjek venligst din sti, eftersom der ikke kunnne findes nogen fil med det navn.");
            }

            Console.WriteLine("################################  Data loaded.  ##############################################");
        }
        #endregion
    }
}
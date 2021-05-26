using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;

namespace Bibliotek
{
    class Program
    {
        static void Main(string[] args)
        {
            #region User priming. Fluff.
            Console.Write("Running initialization test. Please wait.");
            WaitTimer();
            
            Console.WriteLine();
            Console.Write("Making meat-bags wait so they think I'm an advanced program.");
            WaitTimer();

            Console.WriteLine();
            Console.Write("Running sanity checks. Please wait.");
            WaitTimer();
            Console.WriteLine();
            #endregion

            // Del 1 Test
            Console.WriteLine("***** Del 1 udskrifts resultat *****");
            Laaner test = new Laaner(1, "test");
            Console.WriteLine(test.HentLaaner());
            Console.WriteLine("-------------------------------------");
            // Del 2 Test
            List<Laaner> listLaaner = new List<Laaner>();
            listLaaner.Add(new Laaner(2, "Sønderborg"));
            listLaaner.Add(new Laaner(3, "Tønder"));
            listLaaner.Add(new Laaner(666, "Herlev"));
            Console.WriteLine("***** Del 2 udskrifts resultat *****");
            UdskrivLaanere(listLaaner);

            // Del 3 Test
            Console.WriteLine("***** Del 3 udskrifts resultat *****");
            listLaaner[2].Navn = "Karsten Karstensen";
            listLaaner[2].Email = "Karsten@Askefis.muh";
            UdskrivLaanere(listLaaner);

            #region Selvstændig tilføjelse (Validation):
            /*
             * listLaaner[2].Navn = "K4rsten Karstensen"; // Ville fejle da der er valideringstjek ift om navn-strengen indeholder tal.
             * listLaaner[2].Email = "hej"; // Ville ligeledes fejle, da der tjekkes om strengen indeholder et '@', og hvis ikke, om den er sat til den hårdkodede "mangler" kode ("Ikke Angivet")
             */
            #endregion

            #region Valgfri del (Find):
            Console.WriteLine("###### OPTIONAL ######");
            Console.WriteLine("FindLaanere(666):");
            FindLaanere(666);

            void FindLaanere(int laanerNummer)
            {
                Console.WriteLine(listLaaner.Find(x => x.laanerNummer == laanerNummer).HentLaaner());
            }
            #endregion

            #region Valgfri del (Bog):
            List<Bog> bogListe = new List<Bog>();
            bogListe.Add(new Bog("En hest i modvind", "Karsten Karstensen", "12345"));
            bogListe.Add(new Bog("En kæphest i C#", "Ivana Humpalot", "23456"));
            
            // Metoden til at udleje en bog kaldes, og derefter kaldes oversigten over lånere igen, for at vise, at det er registrere.
            UdlaanBog("12345", 3);
            UdlaanBog("23456", 3);
            Console.WriteLine("\n\n**** VALGFRI: BOG ****");
            UdskrivLaanere(listLaaner);
            #endregion

            #region Metode til at udleje/reservere en bog.
            void UdlaanBog(string ISBN, int laanerNummer)
            {
                int idLaaner = listLaaner.FindIndex(x => x.laanerNummer == laanerNummer);
                int idBog = bogListe.FindIndex(x => x.ISBN == ISBN);
                if (bogListe[idBog].udlaant == false && bogListe[idBog].reserveret == false)
                { 
                listLaaner[idLaaner].laanteBoeger.Add(bogListe[idBog]);
                bogListe[idBog].udlaansdato = DateTime.Now;
                bogListe[idBog].udlaant = true;
                }
                else if (bogListe[idBog].udlaant == true && bogListe[idBog].reserveret == false)
                {
                    Console.WriteLine("Bogen er desværre udlånt. Vil du reservere den? [Y/N]");
                    if (Console.ReadLine() == "Y")
                    {
                        bogListe[idBog].reservDato = DateTime.Now.AddMonths(1);
                        bogListe[idBog].reserveret = true;
                    }
                 
                }

            }
            #endregion

            #region Valgfri/Selvstændig (Forsinket aflevering)
            // Bare for at oprette et eksempel:
            bogListe[1].udlaansdato = DateTime.Now.AddDays(10);
            UdskrivLaanere(listLaaner);

            // Reservation
            UdlaanBog("12345", 3);
            UdskrivLaanere(listLaaner);
            #endregion

            #region Menu (efter endt gennemkørsel af ovenstående tests)
            bool contMenu = true;

            // do-while løkken sikrer at menuen gentages indtil brugeren manuelt vælger at afslutte den.
            do
            {
                Console.WriteLine("Vælg venligst en mulighed:");
                Console.WriteLine("[a]: Angiv ny Låner");
                Console.WriteLine("[q]: Gem og Afslut");
                Console.WriteLine("[i]: Indlæs data");

                switch (Console.ReadLine())
                {
                    case "a":
                        OpretLaaner();
                        break;
                    case "q":
                        contMenu = false;
                        Gem();
                        break;
                    case "i":
                        Laes();
                        break;

                }

                void OpretLaaner()
                {
                    Console.Write("Angiv biblioteket: \n>");
                    string l_bib = Console.ReadLine();
                    Console.Write("Angiv Låners Navn: \n>");
                    string l_navn = Console.ReadLine();
                    Console.Write("Angiv Låners Email: \n>");
                    string l_email = Console.ReadLine();

                    // ID'et udregnes dynamisk ud fra størrelsen på listLaaner collectionen, da antallet af elementer i den, tildeles dynamisk ift til tilføjelser.
                    // Dog ikke "idiot-sikker", da det kunne tænkes at en tidligere bruger slettes, hvorved en efterfølgende ny bruger ville
                    // kunne risikeres at få tildelt samme bruger ID
                    int l_id = listLaaner.Count + 1;

                    listLaaner.Add(new Laaner(l_id, l_bib, l_navn, l_email));

                    Console.WriteLine();
                }

            } while (contMenu == true);
            #endregion

            #region Gem og Læs metode
            void Gem()
            {
                // Der angives her en dynamisk sti, den noget lettere men langt mere rigid hårdkodning, eftersom jeg ikke kan forvente, at programmet har adgang til at gemme
                // direkte på C:\ drevet hver gang, og endnu mindre, at vedkommendes lokale brugernavn er det samme som mit.
                string sti = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.txt");
                List<string> tekst = new List<string>();
               
                foreach (Laaner p in listLaaner)
                {
                    tekst.Add(p.HentLaaner());
                }
                // Try-Catch i tilfælde af at ovennævnte sti ikke kan skrives til
                try
                {
                    File.WriteAllLines(sti, tekst, Encoding.UTF8);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Du har ikke tilladelse til at gemme filen i den pågældende sti.");
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Den pågældende sti eksisterer ikke.");
                }
            }

            void Laes()
            {
                string sti = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.txt");
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

        static void UdskrivLaanere(List<Laaner> arrLaaner)
        {
            foreach (Laaner laaner in arrLaaner)
            {
                Console.WriteLine(laaner.HentLaaner());
            }
        }

        #region User priming method.
        static void WaitTimer()
        {
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
        }
        #endregion

       
    }
}


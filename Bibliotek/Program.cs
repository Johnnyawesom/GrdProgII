using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;
using System.Linq;

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

            /// <summary>
            /// Method for renting out/reserving a given book. The method finds the renter via their membernummber (laanerNummer), and the book via its ISBN number.
            /// If the book in question isn't rented nor reserved, the book is added to the rentees list of rented books, and registers the date and time as well as rented status
            /// on the book itself.
            /// Otherwise, if the book is rented out, the user is asked if they wish to reserve it. If they agree, the books entry in the bogListe Collection is registered as reserved,
            /// and the reservation deadline is set to 1 month in the future.
            /// </summary>
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
                Console.WriteLine("[e]: Vis en enkelt bruger");
                Console.WriteLine("[f]: List brugere");
             

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
                    case "e":
                        Console.WriteLine($"Der er i alt {listLaaner.Count} brugere.");
                        Console.WriteLine("Angiv indekset på den bruger du gerne vil se:");
                        int bruger = int.Parse(Console.ReadLine());
                        UdskrivLaaner(listLaaner, bruger);
                        break;
                    case "f":
                        foreach (Laaner p in listLaaner)
                        {
                            string saveLaaner = $"{p.laanerNummer};{p.Navn};{p.Email};{p.bibliotek}";
                            foreach (Bog laantBog in p.laanteBoeger)
                            {
                                saveLaaner += $";{laantBog.ISBN}";
                            }
                            Console.WriteLine(saveLaaner);
                        }
                        break;


                }

                /// <summary>
                /// Method for creating new rentees in the system. It first calculates the ID key for the entry by looking at the Count of the listLaaner collection.
                /// It then creates the entry, fetches the index (as the above method isn't foolproof), and process to set library, name, and email, ensuring that 
                /// the constructor validations are triggered correcly.
                /// </summary>
                /// <seealso cref="Person.Navn"/>
                /// <seealso cref="Person.Email"/>
                void OpretLaaner()
                {
                    
                    // BEMÆRK: Refactored kode. Funktionelt set det samme, men nu anvendes der en FindIndex metode til at finde indekset for den (just forinden) oprettede bruger.
                    // Dette gøres for at de resterende værdier skal sættes for at valideringerne tager effekt, og ikke overføres via parametre i kaldet, 
                    // hvilket forbigår get/set funktionaliteterne.
                    // ID'et udregnes dynamisk ud fra størrelsen på listLaaner collectionen, da antallet af elementer i den, tildeles dynamisk ift til tilføjelser.
                    // Dog ikke "idiot-sikker", da det kunne tænkes at en tidligere bruger slettes, hvorved en efterfølgende ny bruger ville
                    // kunne risikeres at få tildelt samme bruger ID
                    int laanID = (listLaaner.Count) + 1;
                    

                    listLaaner.Add(new Laaner(laanID));
                    int laanIDIndex = listLaaner.FindIndex(x => x.laanerNummer == laanID);
                    Console.Write("Angiv biblioteket: \n>");
                    listLaaner[laanIDIndex].bibliotek = Console.ReadLine();
                    Console.Write("Angiv Låners Navn: \n>");
                    listLaaner[laanIDIndex].Navn = Console.ReadLine();
                    Console.Write("Angiv Låners Email: \n>");
                    listLaaner[laanIDIndex].Email = Console.ReadLine();

                    Console.WriteLine();
                }

            } while (contMenu == true);
            #endregion

            #region Gem og Læs metode
            /// <summary>
            /// Method for saving the list of rentees. It uses a default path (Desktop\test.txt) to store the string, but also includes exception handling in case the path doesn't exist.
            /// </summary>
            void Gem()
            {
                // Der angives her en dynamisk sti, den noget lettere men langt mere rigid hårdkodning, eftersom jeg ikke kan forvente, at programmet har adgang til at gemme
                // direkte på C:\ drevet hver gang, og endnu mindre, at vedkommendes lokale brugernavn er det samme som mit.
                string sti = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.txt");
                List<string> tekst = new List<string>();

                foreach (Laaner p in listLaaner)
                {
                    string saveLaaner = $"{p.laanerNummer};{p.Navn};{p.Email};{p.bibliotek}";
                    foreach (Bog laantBog in p.laanteBoeger)
                    {
                        saveLaaner += $";{laantBog.ISBN}";
                    }
                    tekst.Add(saveLaaner);
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

            /// <summary>
            /// Method for loading data from a txt-file. Each line is read in sequence, and split by each ';' to be stored in a string array.
            /// Each index then corresponds to a certain field (as save and load have been standardized), so each index of the array can
            /// be safely assigned to a given field in the listLaaner collection.
            /// Finally, any books already rented by new entries are added directly to the laanteBoege collection, to avoid
            /// confounding data - such as date of rent, etc.
            /// </summary>
            void Laes()
            {
                string sti = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "tet.txt");
                try
                {
                    // Hver linje fra tekstfilen indlæses som hver sit element i en string array:
                    /*string[] tekst = File.ReadAllLines(sti, Encoding.UTF8);
                    foreach (string s in tekst)
                    {
                        // Hvert element opdeles ved hver ';':
                        string[] data = s.Split(';');
                        if (listLaaner.Any(x => x.laanerNummer == int.Parse(data[0])) == false)     // Der tjekkes om låneren allerede eksisterer i systemet. Her bruges ID og ikke Index
                        {
                            
                            int laanID = listLaaner.Count + 1; // Nyt ID oprettes ud fra størrelsen på listLaaner collectionen.
                            listLaaner.Add(new Laaner(laanID)); // Låneren tilføjes via de nye ID
                            int laanIDIndex = listLaaner.FindIndex(x => x.laanerNummer == laanID); // Og det nye entrys index lokaliseres

                            // Hvorefter vi kan bruge set til at angive værdierne lagret i arrayet, ud fra et standardiseret format (ID;Navn;Email;Bibliotek;Bog;Bog;Bog;[...]
                            listLaaner[laanIDIndex].Navn = data[1]; 
                            listLaaner[laanIDIndex].Email = data[2];
                            listLaaner[laanIDIndex].bibliotek = data[3];
                            
                            // For hvert element udover det fjerde (dvs for hver bog), bruges værdien til at registrere den pågælden bog som udlejet til låneren
                            for (int i = 0; i < (data.Length - 4); i++)
                            {
                                int idBog = bogListe.FindIndex(x => x.ISBN == data[i+4]);
                                listLaaner[laanIDIndex].laanteBoeger.Add(bogListe[idBog]);
                            }
                        }
                    }*/

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
        
        static void UdskrivLaaner(List<Laaner> arrLaaner, int laaner)
        {
            // Bemærk at der her fratrækkes 1, for at gøre funktionaliteten mere almen menneskevenlig (eftersom vi ikke bør antage, 
            // at alle ved, at indeks starter fra 0, men at de fleste antager det starter ved 1.
            Console.WriteLine(arrLaaner[laaner-1].HentLaaner());
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


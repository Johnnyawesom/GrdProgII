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
            Laaner.UdskrivLaanere(listLaaner);

            // Del 3 Test
            Console.WriteLine("***** Del 3 udskrifts resultat *****");
            listLaaner[2].Navn = "Karsten Karstensen";
            listLaaner[2].Email = "Karsten@Askefis.muh";
            Laaner.UdskrivLaanere(listLaaner);
            

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
                Console.WriteLine(listLaaner.Find(x => x.LaanerNummer == laanerNummer).HentLaaner());
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
            Laaner.UdskrivLaanere(listLaaner);
            Bog testBog = new Bog("Test", "Karsten", "34567");
            
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
                int idLaaner = listLaaner.FindIndex(x => x.LaanerNummer == laanerNummer);
                int idBog = bogListe.FindIndex(x => x.ISBN == ISBN);
                if (bogListe[idBog].Udlaant == false && bogListe[idBog].Reserveret == false)
                { 
                    listLaaner[idLaaner].laanteBoeger.Add(bogListe[idBog]);
                    bogListe[idBog].Udlaansdato = DateTime.Now;
                    bogListe[idBog].Udlaant = true;
                }
                else if (bogListe[idBog].Udlaant == true && bogListe[idBog].Reserveret == false)
                {
                    Console.WriteLine("Bogen er desværre udlånt. Vil du reservere den? [Y/N]");
                    if (Console.ReadLine() == "Y")
                    {
                        bogListe[idBog].ReservDato = DateTime.Now.AddMonths(1);
                        bogListe[idBog].Reserveret = true;
                    }
                 
                }

            }
            #endregion

            #region Valgfri/Selvstændig (Forsinket aflevering)
            // Bare for at oprette et eksempel:
            bogListe[1].Udlaansdato = DateTime.Now.AddDays(10);
            Laaner.UdskrivLaanere(listLaaner);

            // Reservation
            UdlaanBog("12345", 3);
            Laaner.UdskrivLaanere(listLaaner);
            #endregion

            #region Menu (efter endt gennemkørsel af ovenstående tests)
            bool contMenu = true;

            // do-while løkken sikrer at menuen gentages indtil brugeren manuelt vælger at afslutte den.
            do
            {
                Console.WriteLine("Vælg venligst en mulighed:");
                Console.WriteLine("\n> Brugere:");
                Console.WriteLine(">> [a]: Angiv ny Låner");
                Console.WriteLine(">> [e]: Vis en enkelt bruger");
                Console.WriteLine(">> [f]: Vise alle brugere");
                Console.WriteLine("\n> Bøger:");
                Console.WriteLine(">> [b]: Vis alle bøger");
                Console.WriteLine(">> [u]: Udlån Bog");
                Console.WriteLine("\n> Funktionelt:");
                Console.WriteLine(">> [q]: Gem og Afslut");
                Console.WriteLine(">> [i]: Indlæs data");
               

             

                switch (Console.ReadLine())
                {
                    case "a":
                        Laaner.OpretLaaner(listLaaner);
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
                        try
                        {
                            Laaner.UdskrivLaaner(listLaaner, bruger);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Brugeren kunne ikke findes. Bemærk venligst at der bedes om indeks nummmer, og ikke Låner ID");
                        }
                        finally
                        {
                            contMenu = true;
                        }
                        break;

                    case "f":
                        foreach (Laaner p in listLaaner)
                        {
                            string saveLaaner = $"{p.LaanerNummer};{p.Navn};{p.Email};{p.Bibliotek}";
                            foreach (Bog laantBog in p.laanteBoeger)
                            {
                                saveLaaner += $";{laantBog.ISBN}";
                            }
                            Console.WriteLine(saveLaaner);
                        }
                        break;

                    case "u":
                        Console.WriteLine("Angiv ISBN-nummer på bogen du gerne vil udlåne (f.eks. 12345):");
                        string uISBN = Console.ReadLine();
                        Console.WriteLine("Angiv Låner-nummeret på den person der vil låne bogen (f.eks. 666):");
                        int uLaaner = int.Parse(Console.ReadLine());
                        UdlaanBog(uISBN, uLaaner);
                        break;
                    case "b":
                        Bog.GetBoeger(bogListe);
                        break;
                        
                        
                       
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
                    string saveLaaner = $"{p.LaanerNummer};{p.Navn};{p.Email};{p.Bibliotek}";
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
                string sti = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.txt");
                try
                {
                    // Hver linje fra tekstfilen indlæses som hver sit element i en string array:
                    string[] tekst = File.ReadAllLines(sti, Encoding.UTF8);
                    foreach (string s in tekst)
                    {
                        // Hvert element opdeles ved hver ';':
                        string[] data = s.Split(';');
                        if (listLaaner.Any(x => x.LaanerNummer == int.Parse(data[0])) == false)     // Der tjekkes om låneren allerede eksisterer i systemet. Her bruges ID og ikke Index
                        {
                            
                            int laanID = listLaaner.Count + 1; // Nyt ID oprettes ud fra størrelsen på listLaaner collectionen.
                            listLaaner.Add(new Laaner(laanID)); // Låneren tilføjes via de nye ID
                            int laanIDIndex = listLaaner.FindIndex(x => x.LaanerNummer == laanID); // Og det nye entrys index lokaliseres

                            // Hvorefter vi kan bruge set til at angive værdierne lagret i arrayet, ud fra et standardiseret format (ID;Navn;Email;Bibliotek;Bog;Bog;Bog;[...])
                            listLaaner[laanIDIndex].Navn = data[1]; 
                            listLaaner[laanIDIndex].Email = data[2];
                            listLaaner[laanIDIndex].Bibliotek = data[3];
                            
                            // For hvert element udover det fjerde (dvs for hver bog), bruges værdien til at registrere den pågælden bog som udlejet til låneren
                            for (int i = 0; i < (data.Length - 4); i++)
                            {
                                int idBog = bogListe.FindIndex(x => x.ISBN == data[i+4]);
                                listLaaner[laanIDIndex].laanteBoeger.Add(bogListe[idBog]);
                            }
                        }
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

        


        #region User priming method.
        static void WaitTimer()
        {
           
            Console.Write(".");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(200);
            Console.Write(".");
            Thread.Sleep(200);
        }
        #endregion

       
    }
}


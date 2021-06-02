using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek
{

    public class Laaner : Person
    {
        public int LaanerNummer { get; set; }
        public string Bibliotek { get; set; }

        // Del 2 (base delen tilhører Del 3)
        // Constructor der instantiere et Låner objekt med de angivne argumenter.
        public Laaner(int laanerNummer, string bibliotek, string navn, string email) : base(navn, email)
        {
            this.LaanerNummer = laanerNummer;
            this.Bibliotek = bibliotek;
        }
        public Laaner(int laanerNummer, string bibliotek)
        {
            this.LaanerNummer = laanerNummer;
            this.Bibliotek = bibliotek;
        }
        public Laaner(int laanerNummer)
        {
            this.LaanerNummer = laanerNummer;
        }
        // Metode der returnere en string, hvori der er angivet den pågældende låner (og dennes bibliotek, antageligvis).
        // Valgfri (Bog) Tilføjet kode så det også returnere bøger og dato for udlejning.
        #region HentLaaner metode
        public string HentLaaner()
        {
            if (Navn == null)
                Navn = "Ikke Angivet";
            if (Email == null)
                Email = "Ikke Angivet";
            string result = $"Navn: {Navn} \tLåner: {LaanerNummer} \tBibliotek: {Bibliotek} \tEmail: {Email} ";
            result += $"\nLånte bøger: \t\tUdlånsdato:";
            foreach (Bog bog in laanteBoeger)
            {
                result += $"\n- {bog.Titel} \t{bog.Udlaansdato}";
                // If-statement til at tjekke dato den pågældende bog blev udlejet.
                if (bog.Udlaansdato.Subtract(DateTime.Now).Days > 7)
                    result += " FORÆLDET!";
                // For at tjekke om bogen er reserveret.
                if (bog.Reserveret == true)
                    result += $" Reserveret: {bog.ReservDato}";
            }
            result += "\n";
            return result;
        }
        public static void UdskrivLaanere(List<Laaner> arrLaaner)
        {
            foreach (Laaner laaner in arrLaaner)
            {
                Console.WriteLine(laaner.HentLaaner());
            }
        }

        public static void UdskrivLaaner(List<Laaner> arrLaaner, int laaner)
        {
            // Bemærk at der her fratrækkes 1, for at gøre funktionaliteten mere almen menneskevenlig (eftersom vi ikke bør antage, 
            // at alle ved, at indeks starter fra 0, men at de fleste antager det starter ved 1.
            Console.WriteLine(arrLaaner[laaner - 1].HentLaaner());
        }
        /// <summary>
        /// Method for creating new rentees in the system. It first calculates the ID key for the entry by looking at the Count of the listLaaner collection.
        /// It then creates the entry, fetches the index (as the above method isn't foolproof), and process to set library, name, and email, ensuring that 
        /// the constructor validations are triggered correcly.
        /// </summary>
        /// <seealso cref="Person.Navn"/>
        /// <seealso cref="Person.Email"/>
        public static void OpretLaaner(List<Laaner> listLaaner)
        {

            // BEMÆRK: Refactored kode. Funktionelt set det samme, men nu anvendes der en FindIndex metode til at finde indekset for den (just forinden) oprettede bruger.
            // Dette gøres for at de resterende værdier skal sættes for at valideringerne tager effekt, og ikke overføres via parametre i kaldet, 
            // hvilket forbigår get/set funktionaliteterne.
            // ID'et udregnes dynamisk ud fra størrelsen på listLaaner collectionen, da antallet af elementer i den, tildeles dynamisk ift til tilføjelser.
            // Dog ikke "idiot-sikker", da det kunne tænkes at en tidligere bruger slettes, hvorved en efterfølgende ny bruger ville
            // kunne risikeres at få tildelt samme bruger ID
            int laanID = (listLaaner.Count) + 1;


            listLaaner.Add(new Laaner(laanID));
            int laanIDIndex = listLaaner.FindIndex(x => x.LaanerNummer == laanID);
            Console.Write("Angiv biblioteket: \n>");
            listLaaner[laanIDIndex].Bibliotek = Console.ReadLine();
            Console.Write("Angiv Låners Navn: \n>");
            listLaaner[laanIDIndex].Navn = Console.ReadLine();
            Console.Write("Angiv Låners Email: \n>");
            listLaaner[laanIDIndex].Email = Console.ReadLine();

            Console.WriteLine();
        }

        #endregion
    }
}

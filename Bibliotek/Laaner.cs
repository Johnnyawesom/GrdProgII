using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek
{
    class Laaner : Person
    {
        public int laanerNummer { get; set; }
        public string bibliotek { get; set; }

        // Del 2 (base delen tilhører Del 3)
        // Constructor der instantiere et Låner objekt med de angivne argumenter.
        public Laaner(int laanerNummer, string bibliotek, string navn, string email) : base(navn, email)
        {
            this.laanerNummer = laanerNummer;
            this.bibliotek = bibliotek;
        }
        public Laaner(int laanerNummer, string bibliotek)
        {
            this.laanerNummer = laanerNummer;
            this.bibliotek = bibliotek;
        }
        public Laaner(int laanerNummer)
        {
            this.laanerNummer = laanerNummer;
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
            string result = $"Navn: {Navn} \tLåner: {laanerNummer} \tBibliotek: {bibliotek} \tEmail: {Email} ";
            result += $"\nLånte bøger: \t\tUdlånsdato:";
            foreach (Bog bog in laanteBoeger)
            {
                result += $"\n- {bog.titel} \t{bog.udlaansdato}";
                // If-statement til at tjekke dato den pågældende bog blev udlejet.
                if (bog.udlaansdato.Subtract(DateTime.Now).Days > 7)
                    result += " FORÆLDET!";
                // For at tjekke om bogen er reserveret.
                if (bog.reserveret == true)
                    result += $" Reserveret: {bog.reservDato}";
            }
            result += "\n";
            return result;
        }
        #endregion
    }
}

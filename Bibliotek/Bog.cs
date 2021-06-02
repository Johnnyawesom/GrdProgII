using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek
{
    public class Bog 
    {
        public string Titel { get; }
        public string Forfatter { get; }
        public string ISBN { get; }
        public DateTime Udlaansdato { get; set; }
        public bool Udlaant { get; set; }
        public DateTime ReservDato { get; set; }
        public bool Reserveret { get; set; }

        public Bog (string titel, string forfatter, string ISBN)
        {
            this.Titel = titel;
            this.Forfatter = forfatter;
            this.ISBN = ISBN;
        }
        
        
        public static void GetBoeger(List<Bog> boeger)
        {
            foreach (Bog b in boeger)
            {
                Console.WriteLine($"Titel: {b.Titel}\tISBN: {b.ISBN}\tUdlånt: {b.Udlaant}\tAfleveringsfrist: {b.Udlaansdato.AddDays(30)}");
            }
        }
    }
}

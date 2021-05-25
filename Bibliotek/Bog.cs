using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotek
{
    class Bog 
    {
        public string titel { get; }
        public string forfatter { get; }
        public string ISBN { get; }
        public DateTime udlaansdato { get; set; }
        public bool udlaant { get; set; }
        public DateTime reservDato { get; set; }
        public bool reserveret { get; set; }

        public Bog (string titel, string forfatter, string ISBN)
        {
            this.titel = titel;
            this.forfatter = forfatter;
            this.ISBN = ISBN;
        }
    }
}

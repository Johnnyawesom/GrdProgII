using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bibliotek
{
    class Person
    {
        string _navn = default;
        string _email = default;
        public List<Bog> laanteBoeger { get; set; } = new List<Bog>();

        // Her bruges der "_<variable>" til at kende forskel felter og egenskaber, i stedet for "this.<variable>".
        public Person(string navn, string email)
        {
            _navn = navn;
            _email = email;
        }

        // Oprettet denne constructor for at gøre det muligt at oprette en bruger uden navn og email (i et virkeligt scenarie ville det ikke være hensigstsmæssigt
        // så blot for sjovs skyld her.
        public Person()
        {

        }

        public string Navn
        {
            get
            {
                return _navn;
            }
            set
            {
                // Validering for at tjekke om navnet er gyldigt (udenfor nummerologiske fanatikerer)
                if (value.Any(char.IsDigit) == false)
                    _navn = value;
            
                else
                    throw new ArgumentException("Navnet må ikke indeholde tal.");
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {   
                // Validering for at tjekke det er en gyldig email (vi ser bort fra .com osv), eller om den blot ikke er angivet.
                if (value.Contains("@") == true || value == "Ikke Angivet")
                    _email = value;
                else
                    throw new ArgumentException("Ugyldig Email");
            }
        }
    
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bibliotek
{
    public class Person
    {
        string _navn = default;
        string _email = default;

        // Oprettes en collection til at indeholde udlånte bøger
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

        #region "Navn" Properties
        public string Navn
        {
            get
            {
                return _navn;
            }
            set
            {
                #region Legacy code (Validering)
                // Validering for at tjekke om navnet er gyldigt (udenfor nummerologiske fanatikerer). Anvendes en Throw i stedet for Try..Catch..Finally, 
                // Eftersom det ikke ville være ugyldigt hvis der blev indtastet et ciffer i en streng, som så ikke ville generere en fejl.
                /*if (value.Any(char.IsDigit) == false)
                    _navn = value;
            
                else
                    throw new ArgumentException("Navnet må ikke indeholde tal.");*/
                #endregion

                // Validering sker ved at koden indsættes i en do..while løkke, som afvikles indtil input godtages (i dette tilfælde, at navnet ikke indeholder tal)
                bool contName = true;
                do
                {
                    if (value.Any(char.IsDigit) == false)
                    {
                        _navn = value;
                        contName = false;
                    }

                    else
                    {
                        Console.WriteLine("Navnet må ikke indeholde tal.");
                        value = Console.ReadLine();
                        contName = true;
                    }
                } while (contName == true);
            }
        }
        #endregion

        #region "Email" Properties
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                #region Legacy code
                // Validering for at tjekke det er en gyldig email (vi ser bort fra .com osv), eller om den blot ikke er angivet.
                /*if (value.Contains("@") == true || value == "Ikke Angivet")
                    _email = value;
                else
                    throw new ArgumentException("Ugyldig Email");*/
                #endregion
                // Validering via do-while løkke, i kræft af bools værdi der tjekkes hver gennemløb, og ændres afhængig af udfaldet af det if-statement der er inde i løkken.
                bool contEmail = true;
                do
                {
                    if (value.Contains("@") == true || value == "Ikke Angivet") 
                    {
                        _email = value;
                        contEmail = false;
                    }
                    else
                    {
                        Console.WriteLine("Ugyldig Email. Angiv venligst en ny:");
                        value = Console.ReadLine();
                        contEmail = true;
                        
                        
                    }
                } while (contEmail == true);
            }
        }
        #endregion
    }

}

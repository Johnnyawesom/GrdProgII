using System;

namespace Kondital
{
    class Program
    {
        static void Main(string[] args)
        {
            double vægt = default;
            double hPuls = default;
            double mPuls = default;
            bool køn = default;
            int alder = default;

            Console.WriteLine("Angiv Hvile Puls:");
            hPuls = double.Parse(Console.ReadLine());
            Console.WriteLine("Angiv Maksimal Puls:");
            mPuls = double.Parse(Console.ReadLine());
            Console.WriteLine("Angiv vægt (undskyld):");
            vægt = double.Parse(Console.ReadLine());
            Console.WriteLine("Hvor gammel er du? (had mig ej!)");
            alder = int.Parse(Console.ReadLine());
            Console.WriteLine("Og endelig, er du kvinde eller mand? [K/M]");
            køn = (Console.ReadLine() == "K") ? true : false;

            Kondital maalEt = new Kondital(hPuls, mPuls, vægt, alder, køn);

            Console.WriteLine($"Dit kondital er: \t{maalEt.CalcKondi()} ml/kg/min");
            Console.WriteLine($"Din iltoptagelse er: \t{maalEt.CalcO2()} l/ml");
            Console.WriteLine($"Kondi-tilstand: \t{maalEt.Tilstand()}");

        }
    }

    class Kondital
    {
        double _hPuls = default;
        double _mPuls = default;
        double _vægt = default;
        double _avgPuls = default;
        int _alder = default;
        bool _køn = default;

        public Kondital(double hPuls, double mPuls, double vægt, int alder, bool køn)
        {
            _hPuls = hPuls;
            _mPuls = mPuls;
            _vægt = vægt;
            _alder = alder;
            _køn = køn;
        }

        // Formlen for Kondital: (Maks-puls / Hvile-puls) * 15,3
        public double CalcKondi()
        {
            _avgPuls = _mPuls / _hPuls * 15.3;
            return Math.Round(_avgPuls);
        }

        // Formlen for Iltoptagelse er Konditallet gange med vægten, divideret med 1000
        public double CalcO2()
        {
            double _oxy = _avgPuls * _vægt / 1000;
            return Math.Round(_oxy, 1);
        }


        /* Bemærk: Da det ikke var muligt at finde frem til formlen bag den angivne tabel, var der ikke umiddelbart en nemmere måde end nedstående.
         * Såfremt formlen kunne findes, kunne værdierne indsættes der, hvorefter alder og køn formentlig ville fungere som justeringer ift normal værdierne
         * og man ville kunne nøjes med en enkelt if-statement af typen nedenunder, da de resulterende værdier ville være korrigeret således, at en 25 årig kvinde
         * med en gennemsnitspuls på 47 ("God"), ville, hvis kvinden havde været 35, resulteret i en gennemsnitspuls på  45 ("God").
         
         * Derudover ser det ud til, at tabellen i sin tid blev udformet absolut. Dvs værdierne blev fastsat ud fra middelværdierne observerede hos testpersoner, 
         * og at dette derudover kan variere fra land til land, hvorfor der ikke 
         
         * På den positive side kan man dog sige, at det er et godt eksempel på metoders styrke: Jeg skal kun hamre tabellen ind én gang, og så kan jeg fremover altid 
         * kalde metoden, hvis jeg skulle få brug for den igen. Omend denne løsning ikke er elegant, er den effektiv.
         
         * Derudover bør det bemærkes, at der er rettet en (sandsynlig) slåfejl i tabellen, i det den angiver intervallet for "Ret Lav" for kvinder mellem 30-39 år
         * som værende "28-22" 
         
         * Endelig er det værd at bemærke, at der i tabellen, for eksempel ved kvinder mellem 30-39 år, står at "Lav" er mindre end 27, og "Ret Lav" er 28-33
         * Det er dermed ikke tydeligt hvad decimal intervallet mellem de to stadier anses som værende (27,00_1 til 27,99_), så i metoden er det vurderet,
         * at den højeste værdi for hver interval er skillelinjen, hvorfor den nedre grænse anses som gående fra den forrige kategoriserings højeste værdi, altså:
         * 28, 29 - 34, 34 - 43 => 
         * 28, 28,00_1 - 34, 34,00_1 - 42 <=> 
         * 28, >28 - 34, >34 - 43 */
        public string Tilstand()
        {
            string tilstand = default;

            switch (_køn)
            {

                case true:

                    if (_alder >= 20 && _alder <= 29)
                    {
                        if (_avgPuls <= 28)
                            tilstand = "Lav";
                        if (_avgPuls > 28 && _avgPuls <= 34)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 34 && _avgPuls <= 43)
                            tilstand = "Middel";
                        if (_avgPuls > 43 && _avgPuls <= 48)
                            tilstand = "God";
                        if (_avgPuls > 48)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 30 && _alder <= 39)
                    {
                        if (_avgPuls <= 27)
                            tilstand = "Lav";
                        if (_avgPuls > 27 && _avgPuls <= 33)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 33 && _avgPuls <= 41)
                            tilstand = "Middel";
                        if (_avgPuls > 41 && _avgPuls <= 47)
                            tilstand = "God";
                        if (_avgPuls > 47)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 40 && _alder <= 49)
                    {
                        if (_avgPuls <= 25)
                            tilstand = "Lav";
                        if (_avgPuls > 25 && _avgPuls <= 31)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 31 && _avgPuls <= 40)
                            tilstand = "Middel";
                        if (_avgPuls > 40 && _avgPuls <= 45)
                            tilstand = "God";
                        if (_avgPuls > 45)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 50 && _alder <= 65)
                    {
                        if (_avgPuls <= 21)
                            tilstand = "Lav";
                        if (_avgPuls > 21 && _avgPuls <= 28)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 28 && _avgPuls <= 36)
                            tilstand = "Middel";
                        if (_avgPuls > 36 && _avgPuls <= 41)
                            tilstand = "God";
                        if (_avgPuls > 41)
                            tilstand = "Meget God";
                    }
                    break;

                case false:
                    if (_alder >= 20 && _alder <= 29)
                    {
                        if (_avgPuls <= 38)
                            tilstand = "Lav";
                        if (_avgPuls > 38 && _avgPuls <= 43)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 43 && _avgPuls <= 51)
                            tilstand = "Middel";
                        if (_avgPuls > 51 && _avgPuls <= 56)
                            tilstand = "God";
                        if (_avgPuls > 56)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 30 && _alder <= 39)
                    {
                        if (_avgPuls <= 34)
                            tilstand = "Lav";
                        if (_avgPuls > 34 && _avgPuls <= 39)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 39 && _avgPuls <= 47)
                            tilstand = "Middel";
                        if (_avgPuls > 47 && _avgPuls <= 51)
                            tilstand = "God";
                        if (_avgPuls > 51)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 40 && _alder <= 49)
                    {
                        if (_avgPuls <= 30)
                            tilstand = "Lav";
                        if (_avgPuls > 30 && _avgPuls <= 35)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 35 && _avgPuls <= 43)
                            tilstand = "Middel";
                        if (_avgPuls > 43 && _avgPuls <= 47)
                            tilstand = "God";
                        if (_avgPuls > 47)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 50 && _alder <= 59)
                    {
                        if (_avgPuls <= 25)
                            tilstand = "Lav";
                        if (_avgPuls > 25 && _avgPuls <= 31)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 31 && _avgPuls <= 39)
                            tilstand = "Middel";
                        if (_avgPuls > 39 && _avgPuls <= 43)
                            tilstand = "God";
                        if (_avgPuls > 43)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 60 && _alder <= 69)
                    {
                        if (_avgPuls <= 21)
                            tilstand = "Lav";
                        if (_avgPuls > 21 && _avgPuls <= 26)
                            tilstand = "Ret Lav";
                        if (_avgPuls > 26 && _avgPuls <= 35)
                            tilstand = "Middel";
                        if (_avgPuls > 35 && _avgPuls <= 39)
                            tilstand = "God";
                        if (_avgPuls > 39)
                            tilstand = "Meget God";
                    }
                    break;

            }
            return tilstand;
        }
    }
}

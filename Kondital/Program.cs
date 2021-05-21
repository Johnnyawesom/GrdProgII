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

        public string Tilstand()
        {
            string tilstand = default;

            switch (_køn)
            {
                case true:
                  
                if (_alder >= 20 && _alder <= 29)
                {
                    if (_avgPuls <= 38)
                        tilstand = "Lav";
                    if (_avgPuls >= 39 && _avgPuls <= 43)
                        tilstand = "Ret Lav";
                    if (_avgPuls >= 35 && _avgPuls <= 51)
                        tilstand = "Middel";
                    if (_avgPuls >= 44 && _avgPuls <= 56)
                        tilstand = "God";
                    if (_avgPuls >= 49)
                        tilstand = "Meget God";
                }
                if (_alder >= 30 && _alder <= 39)
                {
                    if (_avgPuls <= 27)
                        tilstand = "Lav";
                    if (_avgPuls >= 28 && _avgPuls <= 32)
                        tilstand = "Ret Lav";
                    if (_avgPuls >= 34 && _avgPuls <= 41)
                        tilstand = "Middel";
                    if (_avgPuls >= 42 && _avgPuls <= 47)
                        tilstand = "God";
                    if (_avgPuls >= 48)
                        tilstand = "Meget God";
                }
                if (_alder >= 40 && _alder <= 49)
                {
                    if (_avgPuls <= 25)
                        tilstand = "Lav";
                    if (_avgPuls >= 26 && _avgPuls <= 31)
                        tilstand = "Ret Lav";
                    if (_avgPuls >= 32 && _avgPuls <= 40)
                        tilstand = "Middel";
                    if (_avgPuls >= 41 && _avgPuls <= 45)
                        tilstand = "God";
                    if (_avgPuls >= 46)
                        tilstand = "Meget God";
                }
                if (_alder >= 50 && _alder <= 65)
                {
                    if (_avgPuls <= 21)
                        tilstand = "Lav";
                    if (_avgPuls >= 22 && _avgPuls <= 28)
                        tilstand = "Ret Lav";
                    if (_avgPuls >= 29 && _avgPuls <= 36)
                        tilstand = "Middel";
                    if (_avgPuls >= 37 && _avgPuls <= 41)
                        tilstand = "God";
                    if (_avgPuls >= 42)
                        tilstand = "Meget God";
                }
                    break;

                case false:
                    if (_alder >= 20 && _alder <= 29)
                    {
                        if (_avgPuls <= 28)
                            tilstand = "Lav";
                        if (_avgPuls >= 29 && _avgPuls <= 34)
                            tilstand = "Ret Lav";
                        if (_avgPuls >= 35 && _avgPuls <= 43)
                            tilstand = "Middel";
                        if (_avgPuls >= 44 && _avgPuls <= 48)
                            tilstand = "God";
                        if (_avgPuls >= 49)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 30 && _alder <= 39)
                    {
                        if (_avgPuls <= 27)
                            tilstand = "Lav";
                        if (_avgPuls >= 28 && _avgPuls <= 32)
                            tilstand = "Ret Lav";
                        if (_avgPuls >= 34 && _avgPuls <= 41)
                            tilstand = "Middel";
                        if (_avgPuls >= 42 && _avgPuls <= 47)
                            tilstand = "God";
                        if (_avgPuls >= 48)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 40 && _alder <= 49)
                    {
                        if (_avgPuls <= 25)
                            tilstand = "Lav";
                        if (_avgPuls >= 26 && _avgPuls <= 31)
                            tilstand = "Ret Lav";
                        if (_avgPuls >= 32 && _avgPuls <= 40)
                            tilstand = "Middel";
                        if (_avgPuls >= 41 && _avgPuls <= 45)
                            tilstand = "God";
                        if (_avgPuls >= 46)
                            tilstand = "Meget God";
                    }
                    if (_alder >= 50 && _alder <= 65)
                    {
                        if (_avgPuls <= 21)
                            tilstand = "Lav";
                        if (_avgPuls >= 22 && _avgPuls <= 28)
                            tilstand = "Ret Lav";
                        if (_avgPuls >= 29 && _avgPuls <= 36)
                            tilstand = "Middel";
                        if (_avgPuls >= 37 && _avgPuls <= 41)
                            tilstand = "God";
                        if (_avgPuls >= 42)
                            tilstand = "Meget God";
                    }
                    break;
            }
        }
    }
}

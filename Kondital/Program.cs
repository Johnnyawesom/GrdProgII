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

            Console.WriteLine("Angiv Hvile Puls:");
            hPuls = double.Parse(Console.ReadLine());
            Console.WriteLine("Angiv Maksimal Puls:");
            mPuls = double.Parse(Console.ReadLine());
            Console.WriteLine("Angiv vægt (undskyld):");
            vægt = double.Parse(Console.ReadLine());

            Kondital maalEt = new Kondital(hPuls, mPuls, vægt);
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

        
        public Kondital(double hPuls, double mPuls, double vægt)
        {
            _hPuls = hPuls;
            _mPuls = mPuls;
            _vægt = vægt;
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
    }
}

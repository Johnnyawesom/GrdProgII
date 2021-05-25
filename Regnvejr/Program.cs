using System;
using System.Collections.Generic;
using System.Linq;

namespace Regnvejr
{
    class Program
    {
        static void Main(string[] args)
        {
            int day = default;
            List<double> periodAmount = new List<double>();

            Console.WriteLine("Hvor mange dage du vil angive nedbør for:");
            day = int.Parse(Console.ReadLine());

            int[] period = new int[day];
            periodAmount = Rain(period);

            rainResult(periodAmount);
            

            // Metode til at overføre de angivne værdier til en collection, så brugeren kan angive for så mange eller få dage som de ønsker:
            static List<double> Rain(int[] _period)
            {
                List<double> _periodAmount = new List<double>();
                try
                {
                    for (int i = 0; i < _period.Length; i++)
                    {
                        Console.WriteLine($"Angiv venligst nedbør for dag {i + 1}, i milimeter:");
                        _periodAmount.Add(double.Parse(Console.ReadLine()));
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Du glemte at angive en værdi. Skriv venligst \"0\" hvis der ikke var noget nedbør den dag.");
                }

                // Bemærk jeg kunne have brugt nedenstående, men da foreach iterærer gennem listen, returnere "{day}" ikke et index, hvilket gør det mindre
                // gennemskueligt for brugeren (dvs. hvilken dag der er snak om)
                
                /*foreach (int day in _period)
                {
                    Console.WriteLine($"Angiv venligst nedbør i milimeter:");
                    _periodAmount.Add(double.Parse(Console.ReadLine()));
                }*/

                return _periodAmount;
            }
       
            // Metode til at udregne resultatet, dvs. daglig nedbør, gennemsnittet, maksimal og minimal værdierne.
            // Derudover også noget funktionelt overflødig kode, for at formatere udskriften til at være mere æstetisk tiltalende (og læsevenlig)
            static void rainResult(List<double> allAmount)
            {
                double totalRain = 0;

                Console.WriteLine("*********************************************************");
                Console.WriteLine("* \tDag: \t\tNedbør:    \t\t\t*");
                Console.WriteLine("*-------------------------------------------------------*");

                
                for (int i = 0; i < allAmount.Count; i++)
                {
                    Console.WriteLine($"* \t{i + 1}: \t\t{allAmount[i]} \t\t\t\t*");
                    totalRain += allAmount[i];
                }

                Console.WriteLine("*-------------------------------------------------------*");
                Console.WriteLine($"* Gennemsnit: {totalRain/allAmount.Count:N2} \tMinimum: {allAmount.Min<double>()} \tMaksimum: {allAmount.Max<double>()} \t*");
                Console.WriteLine("*********************************************************");
            }
        }
    }
}

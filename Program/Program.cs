using PudelkoLibrary;
using PudelkoLibrary.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            //KOMPRESJA
            Console.WriteLine("\n--==## [KOMPRESJA] ##==--");
            var p1 = new Pudelko(3, 9, 1, UnitOfMeasure.meter);
            var p2 = p1.Kompresuj();

            Console.WriteLine(p2);

            //LISTA I SORTOWANIE
            Console.WriteLine("\n--==## [LISTA I SORTOWANIE] ##==--");
            var boxList = new List<Pudelko>();
            boxList.Add(new Pudelko(1.5, 2.3, 3.6, UnitOfMeasure.meter));
            boxList.Add(new Pudelko(167, 235, 3111, UnitOfMeasure.milimeter));
            boxList.Add(new Pudelko(4, 4, 4));
            boxList.Add(new Pudelko());
            boxList.Add(new Pudelko(132, 30, unit: UnitOfMeasure.centimeter));
            boxList.Add(new Pudelko(10, unit: UnitOfMeasure.meter));

            foreach (var box in boxList)
                Console.WriteLine(box);

            boxList.Sort(Utility.ComparePudelko);
            Console.WriteLine("\nList was sorted\n");

            foreach (var box in boxList)
                Console.WriteLine(box);
        }
    }

    public static class Utility
    {
        public static Pudelko Kompresuj(this Pudelko pud)
        {
            double edge = Math.Pow(pud.Objetosc, 1.0/3.0);
            return new Pudelko(edge, edge, edge, UnitOfMeasure.meter);
        }

        public static int ComparePudelko(Pudelko p1, Pudelko p2)
        {
            //Objetosc
            int results=(p2.Objetosc).CompareTo(p1.Objetosc);
            if (results != 0) return results;

            //Pole
            results = (p2.Pole).CompareTo(p1.Pole);
            if (results != 0) return results;

            //Suma
            return (p2.A + p2.B + p2.C).CompareTo(p1.A + p1.B + p1.C);  
        }
    }
}
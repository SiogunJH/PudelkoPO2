using PudelkoLibrary;
using PudelkoLibrary.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var temp = new Pudelko(11, 2.0,unit: UnitOfMeasure.milimeter);
            Console.WriteLine(temp);
        }
    }
}
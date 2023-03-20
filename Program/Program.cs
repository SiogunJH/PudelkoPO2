using PudelkoLibrary;
using PudelkoLibrary.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            /*
            [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
            [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)]
            */

            var temp = new Pudelko();
            Console.WriteLine(temp);
            Console.WriteLine(temp.a);
        }
    }
}
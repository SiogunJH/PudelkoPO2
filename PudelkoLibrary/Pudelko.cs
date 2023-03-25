using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PudelkoLibrary.Enums;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        // --==## [CLASS VARIABLES] ##==--
        private readonly int a;
        public double A
        {
            get => SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter);
        }
        private readonly int b;
        public double B
        {
            get => SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter);
        }
        private readonly int c;
        public double C
        {
            get => SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter);
        }
        public double Objetosc
        {
            get => Math.Round(
                SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter) * 
                SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter) * 
                SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter),
                9);
        }
        public double Pole
        {
            get => Math.Round(
                2 * (
                (SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter) * SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter)) +
                (SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter) * SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter)) +
                (SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter) * SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter))
                ), 6);
        }

        // --==## [METHODS] ##==--

        //Internal
        private double SizeToSize(double size, UnitOfMeasure unit, UnitOfMeasure desiredUnit) 
            => size * (double)UnitToNumber(unit) / (double)UnitToNumber(desiredUnit);
        private string UnitToString(UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    return "m";
                case UnitOfMeasure.centimeter:
                    return "cm";
                default:
                    return "mm";
            }
        }
        private int UnitToNumber(UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    return 1000;
                case UnitOfMeasure.centimeter:
                    return 10;
                default:
                    return 1;
            }
        }
        private UnitOfMeasure StringToUnit(string unit)
        {
            switch (unit)
            {
                case "m":
                    return UnitOfMeasure.meter;
                case "cm":
                    return UnitOfMeasure.centimeter;
                case "mm":
                    return UnitOfMeasure.milimeter;
                default:
                    throw new FormatException();
            }
        }

        //String related
        public string ToString(string desiredUnit)
        {
            if (desiredUnit == null) desiredUnit = "m";

            switch (desiredUnit)
            {
                case "m":
                    return string.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", desiredUnit, SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter), SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter), SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter));
                case "cm":
                    return string.Format("{1:0.0} {0} × {2:0.0} {0} × {3:0.0} {0}", desiredUnit, SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter), SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter), SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter));
                case "mm":
                    return string.Format("{1:0} {0} × {2:0} {0} × {3:0} {0}", desiredUnit, SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.milimeter), SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.milimeter), SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.milimeter));
                default:
                    throw new FormatException();
            }
        }
        public override string ToString()
            => string.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", UnitToString(UnitOfMeasure.meter), A, B, C);
        public string ToString(string format, IFormatProvider formatProvider)
            => string.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", UnitToString(UnitOfMeasure.meter), A, B, C);

        //Equatable
        public override bool Equals(object other)
        {
            //Check for null
            if (other is null) return false;
            
            //Check for the same reference
            if (ReferenceEquals(this, other)) return true;

            //Check if other is Pudelko
            if (other is Pudelko pudelko) Equals(pudelko);

            //Return false, as other is not Pudelko
            return false;
        }
        public bool Equals(Pudelko other)
        {
            //Check for null
            if (other is null) return false;

            //Check for the same reference
            if (ReferenceEquals(this, other)) return true;

            //Check if objects are equal; Start by creating a List of other Pudelko sizes
            var otherSizes = new List<double> { other.A, other.B, other.C };

            //Then, check if List contains each size - if it does, remove said size from list
            int temp = otherSizes.FindIndex(x => x==A);
            if (temp != -1)
            {
                otherSizes.RemoveAt(temp);
                temp = otherSizes.FindIndex(x => x == B);
                if (temp != -1)
                {
                    otherSizes.RemoveAt(temp);
                    return otherSizes[0] == C;
                }
            }
            return false;
        }
        public override int GetHashCode() 
            => HashCode.Combine(a,b,c);

        // --==## [OPERATORS] ##==--

        public static bool operator ==(Pudelko pud1, Pudelko pud2) => pud1.Equals(pud2);
        public static bool operator !=(Pudelko pud1, Pudelko pud2) => !pud1.Equals(pud2);
        public static Pudelko operator+(Pudelko mainPudelko, Pudelko pudelko)
        {
            //Create an array of possible Pudelko's sizes
            var otherPudelko = new double[6][];
            otherPudelko[0] = new double[3] { pudelko.A, pudelko.B, pudelko.C };
            otherPudelko[1] = new double[3] { pudelko.A, pudelko.C, pudelko.B };
            otherPudelko[2] = new double[3] { pudelko.C, pudelko.A, pudelko.B };
            otherPudelko[3] = new double[3] { pudelko.B, pudelko.A, pudelko.C };
            otherPudelko[4] = new double[3] { pudelko.C, pudelko.B, pudelko.A };
            otherPudelko[5] = new double[3] { pudelko.B, pudelko.C, pudelko.A };

            //Find the smalles Pudelko that will fit both
            var currentSizes = new double[3];
            var smallestSizes = new double[3] {11,11,11}; //Will always be bigger

            for (int i = 0;i< otherPudelko.Length;i++)
            {
                //Move along A edge
                currentSizes[0] = mainPudelko.A + otherPudelko[i][0]; //Sum of A edges from both Pudelko objects
                currentSizes[1] = (mainPudelko.B < otherPudelko[i][1]) ? otherPudelko[i][1] : mainPudelko.B; //The longer B edge
                currentSizes[2] = (mainPudelko.C < otherPudelko[i][2]) ? otherPudelko[i][2] : mainPudelko.C; //The longer C edge
                //Update smallestSizes if needed and if possible
                if (currentSizes[0] * currentSizes[1] * currentSizes[2] < smallestSizes[0] * smallestSizes[1] * smallestSizes[2] && //if currentSize volume is smaller
                    currentSizes[0] <= 10 && currentSizes[1] <= 10 && currentSizes[2] <= 10) //if currentSize are within Pudelko borders
                    for (int j = 0; j < 3; j++)
                        smallestSizes[j] = currentSizes[j];

                //Move along B edge
                currentSizes[1] = mainPudelko.B + otherPudelko[i][1]; //Sum of B edges from both Pudelko objects
                currentSizes[0] = (mainPudelko.A < otherPudelko[i][0]) ? otherPudelko[i][0] : mainPudelko.A; //The longer A edge
                //Update smallestSizes if needed and if possible
                if (currentSizes[0] * currentSizes[1] * currentSizes[2] < smallestSizes[0] * smallestSizes[1] * smallestSizes[2] && //if currentSize volume is smaller
                    currentSizes[0] <= 10 && currentSizes[1] <= 10 && currentSizes[2] <= 10) //if currentSize are within Pudelko borders
                    for (int j = 0; j < 3; j++)
                        smallestSizes[j] = currentSizes[j];

                //Move along C edge
                currentSizes[2] = mainPudelko.C + otherPudelko[i][2]; //Sum of C edges from both Pudelko objects
                currentSizes[1] = (mainPudelko.B < otherPudelko[i][1]) ? otherPudelko[i][1] : mainPudelko.B; //The longer B edge
                //Update smallestSizes if needed and if possible
                if (currentSizes[0] * currentSizes[1] * currentSizes[2] < smallestSizes[0] * smallestSizes[1] * smallestSizes[2] && //if currentSize volume is smaller
                    currentSizes[0] <= 10 && currentSizes[1] <= 10 && currentSizes[2] <= 10) //if currentSize are within Pudelko borders
                    for (int j = 0; j < 3; j++)
                        smallestSizes[j] = currentSizes[j];
            }

            //Return results
            return new Pudelko(smallestSizes[0], smallestSizes[1], smallestSizes[2]);
        }

        // --==## [CONVERSION] ##==--

        public static explicit operator double[](Pudelko pud) => new double[3] { pud.A, pud.B, pud.C };
        
        public static implicit operator Pudelko(ValueTuple<int, int, int> tuple) => new Pudelko(tuple.Item1, tuple.Item2, tuple.Item3, UnitOfMeasure.milimeter);

        public double this[int index]
        {
            get
            {
                switch(index)
                {
                    case 0: return A;
                    case 1: return B;
                    case 2: return C;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public static Pudelko Parse(string text)
        {
            //Split string and validate its length
            string[] size = text.Split('×');
            if (size.Length != 3) throw new FormatException();

            //More validation
            if (
                size[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 2 &&
                size[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 2 &&
                size[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 2
                ) 
                throw new FormatException();

            //Determine the unit
            UnitOfMeasure unit;
            switch (size[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1].Trim())
            {
                case "m":
                    unit = UnitOfMeasure.meter;
                    break;
                case "cm":
                    unit = UnitOfMeasure.centimeter;
                    break;
                case "mm":
                    unit = UnitOfMeasure.milimeter;
                    break;
                default:
                    throw new FormatException();
            }

            //Return parsed object
            return new Pudelko(
                a: double.Parse(size[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[0].Trim()),
                b: double.Parse(size[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[0].Trim()),
                c: double.Parse(size[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)[0].Trim()),
                unit: unit
                );
        }
        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // --==## [CONSTRUCTORS] ##==--
        public Pudelko() : this(0.1, 0.1, 0.1, UnitOfMeasure.meter)
        {
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            // Set default values
            if (a == null) a = SizeToSize(0.1, UnitOfMeasure.meter, unit);
            if (b == null) b = SizeToSize(0.1, UnitOfMeasure.meter, unit);
            if (c == null) c = SizeToSize(0.1, UnitOfMeasure.meter, unit);

            // Assign values
            this.a = (int)Math.Floor(SizeToSize((double)a, unit, UnitOfMeasure.milimeter));
            this.b = (int)Math.Floor(SizeToSize((double)b, unit, UnitOfMeasure.milimeter));
            this.c = (int)Math.Floor(SizeToSize((double)c, unit, UnitOfMeasure.milimeter));

            //Test for values out of range from 1mm to 10000mm
            if (this.a <= 0 || this.b <= 0 || this.c <= 0 || this.a > 10000 || this.b > 10000 || this.c > 10000)
                throw new ArgumentOutOfRangeException();
        }
    }
}